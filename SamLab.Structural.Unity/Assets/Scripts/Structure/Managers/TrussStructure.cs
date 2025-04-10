﻿using System.Collections.Generic;
using Assets.Scripts.Core.Interfaces;
using Assets.Scripts.Structure.Base;
using Assets.Scripts.Structure.Base.Constraints;
using Assets.Scripts.Structure.Base.Loads;
using Assets.Scripts.Structure.Factories;
using Assets.Scripts.Structure.Interfaces;
using Assets.Scripts.Workspace.Geometry.ReferenceGeometry;
using Assets.Scripts.Workspace.Managers;
using UnityEngine;

namespace Assets.Scripts.Structure.Managers
{
    public class TrussStructure : MonoBehaviour
    {
        [SerializeField] public List<ISharedNode> SharedNodes;
        [SerializeField] public List<TrussNode> Nodes;
        [SerializeField] public List<TrussElement> Members;
        [SerializeField] public List<TrussConstraint> Supports;
        [SerializeField] public List<PointLoad> Loads;
        [SerializeField] public float NodeSnapTolerance = 0.1f;
        [SerializeField] public WorkPlane ReferenceWorkPlane;
        private TrussFactory _trussFactory;
        private TrussManager _trussManager;

        public WorkspaceSnapHandler WorkspaceSnapHandler { get; private set; }

        private List<TrussNode> _selectedNodes;
        private List<TrussElement> _selectedMembers;

        public void Initialize(TrussManager manager, TrussFactory trussFactory)
        {
            Nodes ??= new List<TrussNode>();
            Members ??= new List<TrussElement>();
            Supports ??= new List<TrussConstraint>();

            _trussManager = manager;
            _trussFactory = trussFactory;
            WorkspaceSnapHandler = FindFirstObjectByType<WorkspaceSnapHandler>();
        }

        public TrussNode CreateNode(Vector3 position)
        {
            var node = _trussFactory.CreateNode(position, this);
            AddNode(node);

            return node;
        }

        public void CreateMember(TrussNode startNode, TrussNode endNode)
        {
            var element = _trussFactory.CreateMember(startNode, endNode, this);
            AddMember(element);
        }

        public void CreateMember(Vector3 start, Vector3 end)
        {
            var startNode = _trussFactory.CreateNode(start, this);
            var endNode = _trussFactory.CreateNode(end, this);
            AddNode(startNode);
            AddNode(endNode);
            CreateMember(startNode, endNode);
        }

        public void DeleteNode(TrussNode node)
        {
            RemoveNode(node);
            Destroy(node.gameObject); // this shouldn.t be called if using undo/redo
        }

        private void DeactivateNode(TrussNode node)
        {
            if (!_selectedNodes.Contains(node))
                return;
            _selectedNodes.Remove(node);
        }


        public void DeleteMember(TrussElement element)
        {
            RemoveMember(element);
            Destroy(element.gameObject);
        }

        public void AddNode(TrussNode node)
        {
            if (Nodes.Contains(node))
                return;

            Nodes.Add(node);
        }

        public void RemoveNode(TrussNode node)
        {
            if (!Nodes.Contains(node))
                return;
            Nodes.Remove(node);
        }

        private void AddMember(TrussElement element)
        {
            if (Members.Contains(element))
                return;
            Members.Add(element);
        }

        private void RemoveMember(TrussElement member)
        {
            if (!Members.Contains(member))
                return;
            Members.Remove(member);
        }

        private void AddSupport(TrussConstraint support)
        {
            if (Supports.Contains(support))
                return;
            Supports.Add(support);
        }

        private void RemoveSupport(TrussConstraint support)
        {
            if (!Supports.Contains(support))
                return;
            Supports.Remove(support);
        }

        public void AddLoad(PointLoad load)
        {
            if (Loads.Contains(load))
                return;
            Loads.Add(load);
        }

        public void RemoveLoad(PointLoad load)
        {
            if (!Loads.Contains(load))
                return;
            Loads.Remove(load);
        }

    }
}