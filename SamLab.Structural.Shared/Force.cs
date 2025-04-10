﻿using System;

namespace SamLab.Structural.Core
{
    public class Force
    {
        public float X { get; private set; }
        public float Y { get; private set; }

        public float Magnitude => MathF.Sqrt(X * X + Y * Y);
        public Vector2 Direction => Vector2.Normalize(new Vector2(X, Y));

        public Node InsertionNode { get; private set; }

        public Force(float x, float y, Node insertionNode)
        {
            X = x;
            Y = y;
            InsertionNode = insertionNode;
        }

        public Force(float magnitude, float angleRadians, Node insertionNode, bool fromPolar)
        {
            X = magnitude * MathF.Cos(angleRadians);
            Y = magnitude * MathF.Sin(angleRadians);
            InsertionNode = insertionNode;
        }

        public void UpdateComponents(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void UpdateMagnitudeAndAngle(float magnitude, float angleRadians)
        {
            X = magnitude * MathF.Cos(angleRadians);
            Y = magnitude * MathF.Sin(angleRadians);
        }

        public void MoveToNode(Node newNode)
        {
            InsertionNode = newNode;
        }

        public float CalculateMoment(Vector2 pivotPoint) //TODO: Handle this by a StructuralAnalysis class
        {
            Vector2 r = InsertionNode.Position - pivotPoint;
            return Vector2Extension.Cross(r, new Vector2(X, Y));

        }

        public float CalculateMoment(Node pivotNode) //TODO: Handle this by a StructuralAnalysis class
        {
            return CalculateMoment(pivotNode.Position);
        }
    }
}
