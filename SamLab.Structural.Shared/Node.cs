﻿using System.Collections.Generic;

using SamLab.Structural.Core.StructuralElements;

namespace SamLab.Structural.Core
{
    public struct Node
    {
        public Vector2 Position { get; private set; }
        public int Id { get; set; }

        public Vector2 AppliedForce;
        public Vector2 Displacement;

        public List<Member> Members;
        //public List<(int MemberId, int ConnectedNodeId)> ConnectedMembers;

        public Support Support { get; private set; }
        public Node(Vector2 position)
        {
            Position = position;
            Id = GlobalIdHandler.GetNextNodeIndex(); 
            AppliedForce = Vector2.Zero;
            Displacement = Vector2.Zero;
        }

        public void SetForce(Vector2 force)
        {
            AppliedForce = force;
        }

        public void SetPosition(Vector2 position)
        {
            Displacement = position - Position;
            Position = position;
        }

        public void Reset()
        {
            AppliedForce = Vector2.Zero;
            Displacement = Vector2.Zero;
        }

        public void AddMember(Member member)
        {
            if(Members.Contains(member))
                return;

            Members.Add(member);
        }

        public void RemoveMember(Member member)
        {
            if (!Members.Contains(member))
                return;
            Members.Remove(member);
        }

        public void AddSupprot(Support support)
        {
            Support = support;
        }
    }
}
