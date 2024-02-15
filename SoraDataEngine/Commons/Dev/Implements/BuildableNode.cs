using SoraDataEngine.Commons.Condition;
using SoraDataEngine.Commons.Effects;
using SoraDataEngine.Commons.Event;
using SoraDataEngine.Runtime;
using SoraDataEngine.Runtime.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Dev.Implements
{
    /// <summary>
    /// 支持快速构建的基础节点
    /// </summary>
    public class BuildableNode : Node
    {
        /// <summary>
        /// 构建行为
        /// </summary>
        protected Action<object?> _buildAction;
        /// <summary>
        /// 自定义构建行为
        /// </summary>
        protected Action<object?>? _customizedBuildAction;

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="parent">父节点</param>
        /// <param name="customizedBuildAction">自定义构建行为</param>
        public BuildableNode(string name, INode? parent,
            Action<object?>? customizedBuildAction = null) : base(name, parent)
        {
            _customizedBuildAction = customizedBuildAction;
            _buildAction = (object? o) => 
            {
                _customizedBuildAction?.Invoke(o);
                _BuildChildren(o);
            };
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="parent">父节点</param>
        /// <param name="buildAction">构建行为</param>
        /// <param name="customizedBuildAction">自定义构建行为</param>
        public BuildableNode(string name, INode? parent, Action<object?> buildAction,
            Action<object?>? customizedBuildAction = null) : base(name, parent)
        {
            _customizedBuildAction = customizedBuildAction;
            Action<object?> action = buildAction;
            _buildAction = (object? o) => 
            {
                action(o);
                _customizedBuildAction?.Invoke(o);
                _BuildChildren(o);
            };
        }

        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="o">参数</param>
        public virtual void Build(object? o)
        {
            _buildAction(o);
        }

        /// <summary>
        /// 构建所有子节点
        /// </summary>
        /// <param name="o">参数</param>
        protected virtual void _BuildChildren(object? o)
        {
            foreach (var child in Children)
            {
                ((BuildableNode)child).Build(o);
            }
        }
    }

    /// <summary>
    /// 支持快速构建的树形图 IEvent 节点
    /// </summary>
    public class BuildableTreeNode : BuildableNode
    {
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="parent">父节点</param>
        /// <param name="event">IEvent 事件</param>
        /// <param name="customizedBuildAction">自定义构建行为</param>
        public BuildableTreeNode(string name, INode? parent, IEvent @event,
            Action<object?>? customizedBuildAction = null) : 
            base (name, parent, customizedBuildAction)
        {
            Name = name;
            Parent = parent;
            _buildAction = new Action<object?>(
                (object? o) =>
                {
                    RuntimeCore.EventManager?.RegistEvent(@event);
                    _customizedBuildAction?.Invoke(o);
                    _BuildChildren(o);
                });
        }
    }

    /// <summary>
    /// 支持子节点延迟构建的树形图 IEvent 节点
    /// </summary>
    public class DeferredBuildableTreeNode : BuildableTreeNode
    {
        protected ulong _deferredTime, _st;

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="parent">父节点</param>
        /// <param name="event">IEvent 事件</param>
        /// <param name="deferredTime">子节点延迟构建的时间</param>
        /// <param name="customizedBuildAction">自定义构建行为</param>
        public DeferredBuildableTreeNode(string name, INode? parent, IEvent @event, ulong deferredTime,
            Action<object?>? customizedBuildAction = null) : 
            base(name, parent, @event, customizedBuildAction) 
        { 
            _deferredTime = deferredTime;
            _st = 0;
            _buildAction = new Action<object?>(
                (object? o) =>
                {
                    RuntimeCore.EventManager?.RegistEvent(@event);
                    _customizedBuildAction?.Invoke(o);
                    RuntimeCore.EventManager?.RegistEvent(
                        new ScheduledEvent(EventManager.TrueConditionInst, 
                                new IEffect[]
                                {
                                    new Effect(EventManager.TrueConditionInst, new Action<ulong>[]{
                                        new Action<ulong>((ulong time) => 
                                        {
                                            _BuildChildren(o);
                                        })
                                    })
                                }, startTime: _st, endTime: _st
                            )
                        );
                }
                );
        }

        /// <summary>
        /// 构建节点及其子节点
        /// </summary>
        /// <param name="o">参数</param>
        public override void Build(object? o)
        {
            _st = (RuntimeCore.Scheduler?.ElapsedTime ?? 0) + _deferredTime;
            base.Build(o);
        }
    }

    /// <summary>
    /// 支持快速构建的链式 IEvent 节点
    /// </summary>
    public class BuildableChainNode : BuildableNode
    {
        /// <summary>
        /// 链的下一项
        /// </summary>
        public INode? Next { get; set; }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="previous">上一节点</param>
        /// <param name="next">下一节点</param>
        /// <param name="event">IEvent 事件</param>
        /// <param name="customizedBuildAction">自定义构建行为</param>
        public BuildableChainNode(string name, INode? previous, INode? next, IEvent @event,
            Action<object?>? customizedBuildAction = null) : 
            base(name, previous, customizedBuildAction)
        {
            Parent = previous;
            Next = next;
            _buildAction = new Action<object?>(
                (object? o) =>
                {
                    RuntimeCore.EventManager?.RegistEvent(@event);
                    _customizedBuildAction?.Invoke(o);
                    _BuildNext(o);
                });
        }

        /// <summary>
        /// 构建下一节点
        /// </summary>
        /// <param name="o">参数</param>
        protected virtual void _BuildNext(object? o)
        {
            ((BuildableChainNode?)Next)?.Build(o);
        }

        /// <summary>
        /// 不能构建 Children
        /// </summary>
        /// <param name="o"></param>
        /// <exception cref="NotImplementedException"></exception>
        protected override void _BuildChildren(object? o)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 支持延迟构建的链式 IEvent 节点
    /// </summary>
    public class DeferredBuildableChainNode : BuildableChainNode
    {
        protected ulong _deferredTime, _st;

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="previous">上一节点</param>
        /// <param name="next">下一节点</param>
        /// <param name="event">IEvent 事件</param>
        /// <param name="deferredTime">下一项延迟构建的时间</param>
        /// <param name="customizedBuildAction">自定义构建事件</param>
        public DeferredBuildableChainNode(string name, INode? previous, INode? next, IEvent @event, ulong deferredTime,
            Action<object?>? customizedBuildAction = null) : 
            base(name, previous, next, @event, customizedBuildAction)
        {
            _deferredTime = deferredTime;
            _st = 0;
            _buildAction = new Action<object?>(
                (object? o) =>
                {
                    RuntimeCore.EventManager?.RegistEvent(@event);
                    _customizedBuildAction?.Invoke(o);
                    RuntimeCore.EventManager?.RegistEvent(
                        new ScheduledEvent(EventManager.TrueConditionInst,
                                new IEffect[]
                                {
                                    new Effect(EventManager.TrueConditionInst, new Action<ulong>[]{
                                        new Action<ulong>((ulong time) =>
                                        {
                                            _BuildNext(o);
                                        })
                                    })
                                }, startTime: _st, endTime: _st
                            )
                        );
                }
                );
        }

        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="o">参数</param>
        public override void Build(object? o)
        {
            _st = (RuntimeCore.Scheduler?.ElapsedTime ?? 0) + _deferredTime;
            base.Build(o);
        }
    }
}
