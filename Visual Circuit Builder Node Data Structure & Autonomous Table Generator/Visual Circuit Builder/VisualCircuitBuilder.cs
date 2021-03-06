﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Visual_Circuit_Builder
{
    // Modify the if statement to paint the box and line. Have the box and the line move together.
    //
    public partial class VCB_Form : Form
    {
        //string Img_AND = "AND.jpg";
        //Image Img_AND = Image.FromFile("AND.jpg");
        //string Img_NAND = "NAND.jpg";
        //string Img_OR = "OR.jpg";
        //string Img_NOR = "NOR.jpg";
        //string Img_NOT = "NOT.jpg";
        //string Img_XOR = "XOR.jpg";
        //string Img_XNOR = "XNOR.jpg";

        int PaintCalls = 0;
        //string Img_INPUT = "";
        Font drawFont = new Font("Arial", 11);
        SolidBrush drawBrush = new SolidBrush(Color.Black);

        public VCB_Form()
        {
            InitializeComponent();
            //Console.WriteLine("Hello World");
        }

        // The "size" of an object for mouse over purposes.
        private const int object_radius = 3;
        private const int rect_width = 48;
        private const int rect_height = 48;
        private const int leeway = 0;
        private const int D0 = 12;
        private const int D1 = 24;
        private const int D2 = 42;
        private const int D3 = 48;
        private const int D1_Text = 18;
        private const int D2_Text = 8;
        private const int top_connection = 1;
        private const int bottom_connction = 2;
        private int child = 0;
        private int current = 0;
        private int parent = 0;
        private int conn = -1; // 1 = top/middle; 2 = bottom
        private int Size_Of_Array = 0;

        //Null Point 
        private Point Point_NULL = new Point(-1, -1);

        // We're over an object if the distance squared
        // between the mouse and the object is less than this.
        private const int over_dist_squared = (object_radius * object_radius) * 5; //scope in which mouse is over an endpoint. 
        private const int over_gate_dist_squared = ((rect_height - leeway) * (rect_width - leeway)); //scope in which the mouse is over a gate. 

        // The points that make up the line segments.
        private List<Point> Pt1 = new List<Point>();
        private List<Point> Pt2 = new List<Point>();

        //The Nodes that make up the gates
        private Gate[] Gates = new Gate[50];
        //keep track of the last node....
        private int Last = 0;

        // Bools for Drawing.
        private bool IsDrawing = false;
        private bool IsLine = false;
        private bool IsGate = false;
        private bool toggle = false;
        private bool Delete = false;
        private string gateName = "null";
        private Point NewPt1, NewPt2;

        // The segment we're moving or the segment whose end point we're moving.
        private int MovingSegment = -1;
        private int MovingGate = -1;

        // The end point we're moving.
        private bool MovingStartPoint = false;
        private bool MovingGatePoint = false;
        private bool MovingEndPoint = false;

        // The offset from the mouse to the object being moved.
        private int OffsetX, OffsetY;


        /// <summary>
        /// Mouse Click Event Handler. When the user clicks the mouse picCanvas_MouseDown
        /// decide wether 1. The Mouse clicked on a lines endpoint or gates center point 2. Wether 
        /// the mouse is over a line 3. If the intent is to draw something.
        /// </summary>
        /// <param name="sender">refers to the object that invoked the event that fired the event handler</param>
        /// <param name="e">arguments of the event fired</param>
        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            // See what we're over.
            Point hit_point; 
            int segment_number; // for lines
            int gate_number;
            int toggle;


            //Fcn that check if we are over the toggle button
            if (MouseIsOverToggleButton(e.Location)) {
                
                picCanvas.Invalidate(); }
            //Check If the e.X and Y is near an endpoint or gate center point.
            else if (MouseIsOverEndpoint(e.Location, out segment_number, out hit_point)) 
            {
                
                MouseEvent.Text = "MouseIsOverEndPoint";
                // Start moving this end point.
                picCanvas.MouseMove -= picCanvas_MouseMove_NotDown;
                picCanvas.MouseMove += picCanvas_MouseMove_MovingEndPoint;
                picCanvas.MouseUp += picCanvas_MouseUp_MovingEndPoint;

                // Remember the segment number.
                MovingSegment = segment_number;

                // See if we're moving the start end point.
                MovingStartPoint = (Pt1[segment_number].Equals(hit_point));
                MovingEndPoint = (Pt2[segment_number].Equals(hit_point));

                // Remember the offset from the mouse to the point.
                OffsetX = hit_point.X - e.X;
                OffsetY = hit_point.Y - e.Y;
            }
            else if(MouseIsOverGate(e.Location, out gate_number, out hit_point))
            {
                if (Delete)
                {
                    //fcn call for deleting the gate.
                    Delete_Gate(gate_number);
                    Delete = false; 
                    DeleteLabel.Text = "Delete: false";
                }
                else
                {
                    MouseEvent.Text = "MouseIsOverEndPoint";
                    // Start moving this end point.
                    picCanvas.MouseMove -= picCanvas_MouseMove_NotDown;
                    picCanvas.MouseMove += picCanvas_MouseMove_MovingEndPoint;
                    picCanvas.MouseUp += picCanvas_MouseUp_MovingEndPoint;

                    // Remember the segment number.
                    MovingGate = gate_number;

                    // See if we're moving the start end point.
                    MovingGatePoint = (Gates[gate_number].Point.Equals(hit_point));

                    // Remember the offset from the mouse to the point.
                    OffsetX = hit_point.X - e.X;
                    OffsetY = hit_point.Y - e.Y;
                }
            }
            //Check if the e.X and Y is over a line. 
            else if (MouseIsOverSegment(e.Location, out segment_number))
            {
                MouseEvent.Text = "MouseIsOverSegment";
                // Start moving this segment.
                picCanvas.MouseMove -= picCanvas_MouseMove_NotDown;
                picCanvas.MouseMove += picCanvas_MouseMove_MovingSegment;
                picCanvas.MouseUp += picCanvas_MouseUp_MovingSegment;

                // Remember the segment number.
                MovingSegment = segment_number;

                // Remember the offset from the mouse to the segment's first point.
                OffsetX = Pt1[segment_number].X - e.X;
                OffsetY = Pt1[segment_number].Y - e.Y;
            }
            // Drawing a new object.
            else
            {
                MouseEvent.Text = "MouseDown_Beginning_to_Draw";
                // Start drawing a new segment.
                picCanvas.MouseMove -= picCanvas_MouseMove_NotDown;
                picCanvas.MouseMove += picCanvas_MouseMove_Drawing;
                picCanvas.MouseUp += picCanvas_MouseUp_Drawing;

                IsDrawing = true;
                //If the user is drawing a line
                if (!IsGate)
                {
                    IsLine = true;
                    if (MouseIsOverGateEndpoint(e.Location, out hit_point))
                        child = current;
                    NewPt1 = new Point(e.X, e.Y); //First point of the line
                    NewPt2 = new Point(e.X, e.Y); //Last point of the line
                }
                //If the user choose a gate to draw
                else
                {
                    Gates[Last] = new Gate(gateName);
                    Gates[Last].Value = -1;
                    Gates[Last].In1 = -1;
                    Gates[Last].In2 = -1;
                    Gates[Last].Point = new Point(e.X, e.Y);
                }

            }
           
        }
        /// <summary>
        /// Delete fcn meant to remove a gate from the circuit
        /// </summary>
        /// <param name="gate_number">gate to remove</param>
        private void Delete_Gate(int gate_number)
        {
            for(int i = 0; i < Last; i++)
            {
                if(Gates[i].ParentGate == gate_number)
                {
                    Gates[i].ParentGate = -1;
                    Gates[i].Out = -1;
                }
            }
            if (Gates[gate_number].Out == 1)
                Gates[Gates[gate_number].ParentGate].In1 = -1;
            if (Gates[gate_number].Out == 2)
                Gates[Gates[gate_number].ParentGate].In2 = -1;
            Gates[gate_number].Value = -1;
            int count = Gates[gate_number].ParentGate;
            //update all the way

            do
            {
                Gates[count].Value = -1;
                if (Gates[count].Out == 1)
                    Gates[Gates[count].ParentGate].In1 = Gates[count].Value;

                if (Gates[count].Out == 2)
                    Gates[Gates[count].ParentGate].In2 = Gates[count].Value;

                count = Gates[count].ParentGate;
            }
            while (Gates[count].ParentGate != 0 && count != -1);


            //Remove all the lines of that connected gate
            for (int i = 0; i < Pt1.Count; i++)
            {
                if (FindDistanceToPointSquared(new Point((Gates[gate_number].Point.X - D1), (Gates[gate_number].Point.Y - D0)), Pt2[i]) < over_dist_squared)
                {
                    Pt1[i] = new Point(-1, -1);
                    Pt2[i] = new Point(-1, -1);

                }
                if (FindDistanceToPointSquared(new Point((Gates[gate_number].Point.X + D1), Gates[gate_number].Point.Y), Pt2[i]) < over_dist_squared)
                {
                    Pt1[i] = new Point(-1, -1);
                    Pt2[i] = new Point(-1, -1);

                }
                if (FindDistanceToPointSquared(new Point((Gates[gate_number].Point.X - D1), (Gates[gate_number].Point.Y) + D0), Pt2[i]) < over_dist_squared)
                {
                    Pt1[i] = new Point(-1, -1);
                    Pt2[i] = new Point(-1, -1);
                }
                if (FindDistanceToPointSquared(new Point((Gates[gate_number].Point.X - D1), (Gates[gate_number].Point.Y)), Pt2[i]) < over_dist_squared)
                {
                    Pt1[i] = new Point(-1, -1);
                    Pt2[i] = new Point(-1, -1);

                }
                if (FindDistanceToPointSquared(new Point((Gates[gate_number].Point.X + D1), Gates[gate_number].Point.Y), Pt1[i]) < over_dist_squared)
                {
                    Pt1[i] = new Point(-1, -1);
                    Pt2[i] = new Point(-1, -1);

                }
            }


            //Set the deleted gate to null information
            Gates[gate_number].ID = "Null";
            Gates[gate_number].Point = new Point(-1, -1);
            Gates[gate_number].In1 = -1;
            Gates[gate_number].In2 = -1;
            Gates[gate_number].Out = -1;
            Gates[gate_number].ParentGate = -1;
            Gates[gate_number].Value = -1;

            //Calculate the logic of each gate
            for (int i = 0; i < Last; i++)
                GateHasTwoConnections(i);

            picCanvas.Invalidate();

        }
        /// <summary>
        /// RebuildCircuit, goes through the gategiven and updates
        /// all of the parents.
        /// </summary>
        /// <param name="gatenum">Gate that was clicked</param>
        private void RebuildCircuit(int gatenum)
        {
            int i = gatenum;
            if (Gates[i].Out == 1)
                Gates[Gates[i].ParentGate].In1 = Gates[i].Value;

            if (Gates[i].Out == 2)
                Gates[Gates[i].ParentGate].In2 = Gates[i].Value;

            int gate = Gates[i].ParentGate;

            //Go through the parents
            do
            {

                GateHasTwoConnections(gate);

                if (Gates[gate].Out == 1)
                    Gates[Gates[gate].ParentGate].In1 = Gates[gate].Value;

                if (Gates[gate].Out == 2)
                    Gates[Gates[gate].ParentGate].In2 = Gates[gate].Value;

                gate = Gates[gate].ParentGate;
            }
            while (Gates[gate].ParentGate != 0 && gate != -1);
            GateHasTwoConnections(gate);
        }

        private bool MouseIsOverToggleButton(Point location)
        {
            for(int i = 0; i < Last; i++)
            {
                //Toggle Button
                if (FindDistanceToPointSquared(location, new Point((Gates[i].Point.X), (Gates[i].Point.Y - D1))) < over_dist_squared)
                {
                   
                        if (Gates[i].Value == 0)
                        {
                            Gates[i].Value = 1;
                        }
                        else
                        {
                            Gates[i].Value = 0;
                        }

                        if(Gates[i].Out == 1)
                        Gates[Gates[i].ParentGate].In1 = Gates[i].Value;
                    
                        if (Gates[i].Out == 2)
                        Gates[Gates[i].ParentGate].In2 = Gates[i].Value;

                    int gate = Gates[i].ParentGate;

                    //Go through all the gates.
                    do
                    {
                       
                        GateHasTwoConnections(gate);

                        if (Gates[gate].Out == 1)
                            Gates[Gates[gate].ParentGate].In1 = Gates[gate].Value;

                        if (Gates[gate].Out == 2)
                            Gates[Gates[gate].ParentGate].In2 = Gates[gate].Value;

                        gate = Gates[gate].ParentGate;
                    }
                    while (Gates[gate].ParentGate != 0 && gate != -1);
                    GateHasTwoConnections(gate);
                   
                    return true;
                   
                }
            }
            return false;
        }

        //END of MouseDown

        /// <summary>
        /// Mouse Not_Down Event Handler: If the mouse is hovering over the picCanvas
        /// fire this event evertime the mouse moves and check if the mouse is over 
        /// a line endpoint, gate endpoint, gate or segment. 
        /// </summary>
        /// <param name="sender">refers to the object that invoked the event that fired the event handler</param>
        /// <param name="e">arguments of the event fired</param>
        private void picCanvas_MouseMove_NotDown(object sender, MouseEventArgs e)
        {
            Cursor new_cursor = Cursors.Cross;
            MouseEvent.Text = "MouseMove_Up";

            SegmentsTool.Text = "Number of Segments: " + Pt1.Count;
            // See what we're over.
            Point hit_point;
            int segment_number;
           
            MouseCoordinates.Text = "MouseCoord: " + "(" + e.X + "," + e.Y + ")";

            
            if (MouseIsOverEndpoint(e.Location, out segment_number, out hit_point))
            {
                SetMouse(hit_point);
                //Cursor.Position = picCanvas.PointToScreen(hit_point);
                new_cursor = Cursors.Arrow;
            }
            else if (MouseIsOverSegment(e.Location, out segment_number))
            {
                //SetMouse(hit_point);
                //Cursor.Position = picCanvas.PointToScreen(hit_point);
                new_cursor = Cursors.Hand;
            }
            else if(MouseIsOverGate(e.Location, out segment_number, out hit_point))
            {
               //SetMouse(hit_point);
               // Cursor.Position = picCanvas.PointToScreen(hit_point);
                new_cursor = Cursors.NoMove2D;
            }
            else if(MouseIsOverGateEndpoint(e.Location, out hit_point))
            {
                SetMouse(hit_point);
                new_cursor = Cursors.Arrow;
            }
       

            // Set the new cursor.
            if (picCanvas.Cursor != new_cursor)
                picCanvas.Cursor = new_cursor;
        }
        /// <summary>
        /// Fcn: MouseIsOverGateEndPoint. Takes mouse coordinates checks the lists for a matching
        /// pair of points.
        /// 
        /// </summary>
        /// <param name="mouse_pt"> X and Y coordinates from the MouseEventArgs</param>
        /// <param name="hit_pt"> X and Y coordinates were the Mouse approximately clicked/is over.</param>
        /// <returns></returns>
        private bool MouseIsOverGateEndpoint(Point mouse_pt, out Point hit_pt)
        {
            for (int i = 0; i < Last; i++)
            {
                //Check the Lists for a matching pair.
                if (FindDistanceToGateSquared(mouse_pt, Gates[i].Point) < over_gate_dist_squared) //over_gate_dist_squared the size of the gate.
                {

                        MousePointing.Text = "MousePoint: GateEndPoint";
                    /*Check each endpoint*/
                   
                    //Top Connection
                    if (FindDistanceToPointSquared(mouse_pt, new Point((Gates[i].Point.X - D1), (Gates[i].Point.Y - D0))) < over_dist_squared)
                        {
                            hit_pt = new Point((Gates[i].Point.X - D1), (Gates[i].Point.Y - D0));
                            current = i;
                            conn = 1; //top connection
                            return true;
                        }
                        //End Connection
                        if (FindDistanceToPointSquared(mouse_pt, new Point((Gates[i].Point.X + D1), Gates[i].Point.Y)) < over_dist_squared)
                        {
                            hit_pt = new Point((Gates[i].Point.X + D1), Gates[i].Point.Y);
                            current = i;
                            conn = -1;  //this is set at the beginning
                            return true;
                        }
                        //Bottom Connection
                        if (FindDistanceToPointSquared(mouse_pt, new Point((Gates[i].Point.X - D1), (Gates[i].Point.Y) + D0)) < over_dist_squared)
                        {
                            hit_pt = new Point((Gates[i].Point.X - D1), (Gates[i].Point.Y) + D0);
                            current = i;
                            conn = 2; //bottom connection
                            return true;
                        }
                        //Middle Connection
                        if (FindDistanceToPointSquared(mouse_pt, new Point((Gates[i].Point.X - D1), (Gates[i].Point.Y))) < over_dist_squared)
                        {
                            hit_pt = new Point((Gates[i].Point.X - D1), (Gates[i].Point.Y));
                            current = i;
                             conn = 1; //same as the top 
                            return true;
                        }

                }
            }

           // MousePointing.Text = "MousePoint: Line";
            hit_pt = new Point(-1, -1);
            return false; //return the segment number and the first point of that segment

        }

        private int FindDistanceToGateSquared(Point mouse_pt, Point point)
        {
            int dx = mouse_pt.X - point.X;
            int dy = mouse_pt.Y - point.Y;

            return dx * dx + dy * dy;
        }

        /******************************************************************
         * Function: SetMouse
         * Input: (hit_pt) were the endpoint should be
         * Output: Set that Cursors.Position to that hit_pt for easy use
         ******************************************************************/
        private void SetMouse(Point hit_pt)
        {
            //Set X and Y
            Cursor.Position = picCanvas.PointToScreen(hit_pt);
            return;
        }


        /**/
       

        /// <summary>
        /// onClick Mouse Event for the TopMenuStrip
        /// </summary>
        /// <param name="sender"> refers to the object that invoked the event that fired the event handler</param>
        /// <param name="e">arguments of the event fired</param>
        private void onClick(object sender, EventArgs e)
        {
            DrawGate(sender.ToString());
        }
        //Menu Strip on top of the canvas//
        private void DrawGate(string gate)
        {
            if (gate.Equals("AND"))
            {
                IsGate = true;            
                GateToolStrip.Text = "GateTooStrip: AND";
                gateName = "AND";
            }
            else if (gate.Equals("NAND"))
            {
                IsGate = true; //pictureBox.Image = Image.FromFile(Img_NAND);
                GateToolStrip.Text = "GateToolStrip: NAND";
                gateName = "NAND";
            }
            else if (gate.Equals("OR"))
            {
                IsGate = true;
                GateToolStrip.Text = "GateToolStrip: OR";
                gateName = "OR";
            }
            else if (gate.Equals("NOR"))
            {
                IsGate = true;//pictureBox.Image = Image.FromFile(Img_NOR);
                GateToolStrip.Text = "GateToolStrip: NOR";
                gateName = "NOR";
            }
            else if (gate.Equals("NOT"))
            {
                IsGate = true; //pictureBox.Image = Image.FromFile(Img_NOT);
                GateToolStrip.Text = "GateToolStrip: NOT";
                gateName = "NOT";
            }
            else if (gate.Equals("XOR"))
            {
                IsGate = true;//pictureBox.Image = Image.FromFile(Img_XOR);
                GateToolStrip.Text = "GateToolStrip: XOR";
                gateName = "XOR";
            }
            else if (gate.Equals("XNOR"))
            {
                IsGate = true;//pictureBox.Image = Image.FromFile(Img_XNOR);
                GateToolStrip.Text = "GateToolStrip: XNOR";
                gateName = "XNOR";
            }
            else if (gate.Equals("IN"))
            {
                IsGate = true;
                GateToolStrip.Text = "GateToolStrip: IN";
                gateName = "IN";
            }
            else
            {
                IsGate = true;
                GateToolStrip.Text = "GateToolStrip: END";
                gateName = "END";
            }

           // Invalidate();
        }

        // See if the mouse if over a box
        private bool MouseIsOverGate(Point mouse_pt, out int gate_number, out Point hit_pt)
        {
            for (int i = 0; i < Last; i++)
            {
                // Check the starting point.
                if (FindDistanceToPointSquared(mouse_pt, Gates[i].Point) < over_dist_squared) //over_dist_squared size diameter of the circle
                {
                    // We're over this point.
                    gate_number = i;
                    hit_pt = Gates[i].Point;
                    MousePointing.Text = "MousePoint: Gate";
                    return true; //return a -1 to represent no point was found
                    
                }
            }
            gate_number = -1;
            hit_pt = new Point(-1, -1);
            return false; //return a -1 to represent no point was found
        }
        // See if the mouse is over an end point. 
        private bool MouseIsOverEndpoint(Point mouse_pt, out int segment_number, out Point hit_pt)
        {
            for (int i = 0; i < Pt1.Count; i++)
            {
                // Check the starting point.
                if (FindDistanceToPointSquared(mouse_pt, Pt1[i]) < over_dist_squared) //over_dist_squared size diameter of the circle
                {
                    // We're over this point.
                    segment_number = i;
                    hit_pt = Pt1[i];
                    //Check if its a gate
                    MousePointing.Text = "MousePoint: Line";
                    return true; //return the segment number and the first point of that segment
                    
                }

                // Check the end point.
                if (FindDistanceToPointSquared(mouse_pt, Pt2[i]) < over_dist_squared)
                {
                    // We're over this point.
                    MousePointing.Text = "MousePoint: Line";
                    segment_number = i;
                    hit_pt = Pt2[i];
                    return true; //return the segment number and the last point of that segment
                }

             
            }

            segment_number = -1;
            hit_pt = new Point(-1, -1);
            return false; //return a -1 to represent no point was found
        }

        // See if the mouse is over a line segment.
        private bool MouseIsOverSegment(Point mouse_pt, out int segment_number)
        {
            for (int i = 0; i < Pt1.Count; i++)
            {
                // See if we're over the segment.
                PointF closest;
                if (FindDistanceToSegmentSquared(
                    mouse_pt, Pt1[i], Pt2[i], out closest)
                        < over_dist_squared)
                {
                    // We're over this segment.
                    if (Pt2[i] == new Point(-1, -1))
                    {
                        segment_number = -1;
                        return false; //return a -1 to represent no point was found it was a box.
                    }
                    else
                    {
                        segment_number = i;
                        return true; //return the segment which we are closet too. 
                    }
                }
            }

            segment_number = -1;
            return false;
        }

        // Calculate the distance squared between two points.
        private int FindDistanceToPointSquared(Point pt1, Point pt2)
        {

            int dx = pt1.X - pt2.X;
            int dy = pt1.Y - pt2.Y;

            return dx * dx + dy * dy;
        }

        // Calculate the distance squared between
        // point pt and the segment p1 --> p2.
        private double FindDistanceToSegmentSquared(Point pt, Point p1, Point p2, out PointF closest)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            if ((dx == 0) && (dy == 0))
            {
                // It's a point not a line segment.
                closest = p1;
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
                return dx * dx + dy * dy;
            }

            // Calculate the t that minimizes the distance between the endpoint and the startpoint.
            float t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) / (dx * dx + dy * dy);

            // See if this represents one of the segment's
            // end points or a point in the middle.
            if (t < 0)
            {
                closest = new PointF(p1.X, p1.Y);
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
            }
            else if (t > 1)
            {
                closest = new PointF(p2.X, p2.Y);
                dx = pt.X - p2.X;
                dy = pt.Y - p2.Y;
            }
            else
            {
                closest = new PointF(p1.X + t * dx, p1.Y + t * dy);
                dx = pt.X - closest.X;
                dy = pt.Y - closest.Y;
            }

            return dx * dx + dy * dy;
        }
        // We're moving a segment.
        private void picCanvas_MouseMove_MovingSegment(object sender, MouseEventArgs e)
        {
            MouseEvent.Text = "MouseMove_MovingSegment";
            // See how far the first point will move.
            int new_x1 = e.X + OffsetX;
            int new_y1 = e.Y + OffsetY;

            int dx = new_x1 - Pt1[MovingSegment].X;
            int dy = new_y1 - Pt1[MovingSegment].Y;

            if (dx == 0 && dy == 0) return;

            // Move the segment to its new location.
            Pt1[MovingSegment] = new Point(new_x1, new_y1);
            Pt2[MovingSegment] = new Point(
                Pt2[MovingSegment].X + dx,
                Pt2[MovingSegment].Y + dy);

            // Redraw.
            picCanvas.Invalidate();
        }

        // Stop moving the segment.
        private void picCanvas_MouseUp_MovingSegment(object sender, MouseEventArgs e)
        {
            MouseEvent.Text = "MouseUp_MovingSegment";
            // Reset the event handlers.
            picCanvas.MouseMove += picCanvas_MouseMove_NotDown;
            picCanvas.MouseMove -= picCanvas_MouseMove_MovingSegment;
            picCanvas.MouseUp -= picCanvas_MouseUp_MovingSegment;

            // Redraw.
            picCanvas.Invalidate();
        }

        // We're moving an end point.
        /* Input: MouseEvent 
         * Output: call to invalidate
         * Process: Check is the mouse if over the start endpoint or the last endpoint. 
         * When the mouse moves update the endpoint and call invalidate to paint.*/
        private void picCanvas_MouseMove_MovingEndPoint(object sender, MouseEventArgs e)
        {
            Point hit_pt;
            MouseEvent.Text = "MouseMove_MoveingEndPoint";
            // Move the point to its new location.
            if (MovingStartPoint)
            {
                if (MouseIsOverGateEndpoint(e.Location, out hit_pt))
                    SetMouse(hit_pt);
                Pt1[MovingSegment] =
                   new Point(e.X + OffsetX, e.Y + OffsetY);
               
            }
            else if (MovingGatePoint)
            {
                Gates[MovingGate].Point = new Point(e.X + OffsetX, e.Y + OffsetY);
                MouseEvent.Text = "MouseMove_MovingGate";
                for (int i = 0; i < Pt1.Count; i++)
                {
                    if (FindDistanceToPointSquared(new Point((Gates[MovingGate].Point.X - D1), (Gates[MovingGate].Point.Y - D0)), Pt2[i]) < over_dist_squared)
                    {
                        Pt2[i] = new Point((Gates[MovingGate].Point.X - D1), (Gates[MovingGate].Point.Y - D0));

                    }
                    if (FindDistanceToPointSquared(new Point((Gates[MovingGate].Point.X + D1), Gates[MovingGate].Point.Y), Pt2[i]) < over_dist_squared)
                    {
                        Pt2[i] = new Point((Gates[MovingGate].Point.X + D1), Gates[MovingGate].Point.Y);

                    }
                    if (FindDistanceToPointSquared(new Point((Gates[MovingGate].Point.X - D1), (Gates[MovingGate].Point.Y) + D0), Pt2[i]) < over_dist_squared)
                    {
                        Pt2[i] = new Point((Gates[MovingGate].Point.X - D1), (Gates[MovingGate].Point.Y) + D0);
                    }
                    if (FindDistanceToPointSquared(new Point((Gates[MovingGate].Point.X - D1), (Gates[MovingGate].Point.Y)), Pt2[i]) < over_dist_squared)
                    {
                        Pt2[i] = new Point((Gates[MovingGate].Point.X - D1), (Gates[MovingGate].Point.Y));
                    }
                    if (FindDistanceToPointSquared(new Point((Gates[MovingGate].Point.X - D1), (Gates[MovingGate].Point.Y - D0)), Pt1[i]) < over_dist_squared)
                    {
                        Pt1[i] = new Point((Gates[MovingGate].Point.X - D1), (Gates[MovingGate].Point.Y - D0));

                    }
                    if (FindDistanceToPointSquared(new Point((Gates[MovingGate].Point.X + D1), Gates[MovingGate].Point.Y), Pt1[i]) < over_dist_squared)
                    {
                        Pt1[i] = new Point((Gates[MovingGate].Point.X + D1), Gates[MovingGate].Point.Y);

                    }
                    if (FindDistanceToPointSquared(new Point((Gates[MovingGate].Point.X - D1), (Gates[MovingGate].Point.Y) + D0), Pt1[i]) < over_dist_squared)
                    {
                        Pt1[i] = new Point((Gates[MovingGate].Point.X - D1), (Gates[MovingGate].Point.Y) + D0);
                    }
                    if (FindDistanceToPointSquared(new Point((Gates[MovingGate].Point.X - D1), (Gates[MovingGate].Point.Y)), Pt1[i]) < over_dist_squared)
                    {
                        Pt1[i] = new Point((Gates[MovingGate].Point.X - D1), (Gates[MovingGate].Point.Y));
                    }

                }
            }
            else if (MovingEndPoint)
            {
                if (MouseIsOverGateEndpoint(e.Location, out hit_pt))
                    SetMouse(hit_pt);
                Pt2[MovingSegment] =
                    new Point(e.X + OffsetX, e.Y + OffsetY);

            }

            // Redraw.
            picCanvas.Invalidate();
        }

        // Stop moving the end point.
        /*Input: MouseEvent
          Output: Call to invalidate && resets event handler
            - MouseMove_NotDown is set reset to listen to the mouse
            - MouseMove_MovingEndPoint is turned off
            - MouseUp_MovingEndPoint is turned off */
        private void picCanvas_MouseUp_MovingEndPoint(object sender, MouseEventArgs e)
        {
            MouseEvent.Text = "MouseUp_MovingEndPoint";
            // Reset the event handlers.
            picCanvas.MouseMove += picCanvas_MouseMove_NotDown; //set the not down eventhandler to listen for the mouse moving again
            picCanvas.MouseMove -= picCanvas_MouseMove_MovingEndPoint; // stop both eventhanlders from listening to the mouse
            picCanvas.MouseUp -= picCanvas_MouseUp_MovingEndPoint; // ""

            MovingStartPoint = false;
            MovingGatePoint = false;
            MovingEndPoint = false;
           
            // Redraw.
            picCanvas.Invalidate(); //call paintfcn
        }

        // We're drawing a new segment.
        private void picCanvas_MouseMove_Drawing(object sender, MouseEventArgs e)
        {
            Point hit_pt;
            MouseCoordinates.Text = "MouseCoord: " + "(" + e.X + "," + e.Y + ")";
            MouseEvent.Text = "MouseMove_Drawing";
            //Here check if we are near a gate and snap it to the closet endpoint. 
            // Save the new point.
            if (IsLine)
            {
                if (MouseIsOverGateEndpoint(e.Location, out hit_pt))
                    SetMouse(hit_pt);
                NewPt2 = new Point(e.X, e.Y); //as the user moves the mouse update the last point
            }
            if (IsGate)
            { 
                Gates[Last].Point = new Point(e.X, e.Y);
            }
            // Redraw.
            picCanvas.Invalidate();

        }

        //Opens a file containing a circuit table. 
        private void Open_File(object sender, EventArgs e)
        {
            //Ask if they want to save their current circuit. 
            int counter = 0;
            string line;
            string fname = "null";
            int end_of_old_array = Last;
            if (Gates.Length == 0)
            {
                Gates = new Gate[50];
                Last = 0;
                Size_Of_Array = 0;
            }

            //OpenFileDialog credit to chandrashekhar racharla
            //https://www.codeproject.com/Answers/319018/How-to-select-a-file-in-csharp
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter =
               "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.InitialDirectory = "C:\\";
            dialog.Title = "Select a text file";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fname = dialog.FileName;
                
                
            }
            //End of OpenFileDialog

            try
            {
                //Read file
                System.IO.StreamReader file =
                        new System.IO.StreamReader(fname);
                int sizeofcircuit = Last;
                while ((line = file.ReadLine()) != null)
                {
                    PopulateGatesArray(counter, line, Last, sizeofcircuit);
                    counter++;
                    Last++;

                }

                ConnectGates(Last);
                Size_Of_Array = Last;
                file.Close();
            }
            catch
            {
                MessageBox.Show("Please choose a file", "Error No Title",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
           
            System.Console.WriteLine("There were {0} lines.", counter);
            // Suspend the screen.  
            System.Console.ReadLine();
            picCanvas.Invalidate();

        }

        // Stop drawing.
        private void picCanvas_MouseUp_Drawing(object sender, MouseEventArgs e)
        {
            MouseEvent.Text = "MouseUp_Drawing";
            Point hit_pt;

            // Reset the event handlers.
            picCanvas.MouseMove -= picCanvas_MouseMove_Drawing;
            picCanvas.MouseMove += picCanvas_MouseMove_NotDown;
            picCanvas.MouseUp -= picCanvas_MouseUp_Drawing;

            // Create the new segment.
            if (IsLine)
            {
                if(MouseIsOverGateEndpoint(e.Location, out hit_pt))
                {
                    parent = current;
                    if (conn == top_connection)
                    {
                        Gates[child].Out = top_connection;
                        Gates[child].ParentGate = parent;
                        Gates[parent].In1 = Gates[child].Value;
                    }
                    else
                    {
                        Gates[parent].In2 = Gates[child].Value;
                        Gates[child].Out = bottom_connction;
                        Gates[child].ParentGate = parent;
                    }
                    
                }
                if (Gates[parent] != null)
                {
                    if ((Gates[parent].In1 != -1 && Gates[parent].In2 != -1) || Gates[parent].ID == "NOT")
                    {
                        GateHasTwoConnections(parent);
                        RebuildCircuit(parent);
                    }
                    else
                        Gates[parent].Value = -1;
                }
                Pt1.Add(NewPt1);
                Pt2.Add(NewPt2);
            }
            if (IsGate)
            {
                Gates[Last].Point = new Point(e.X, e.Y);
                Last++;
            }
        
            IsDrawing = false;
            IsLine = false;
            IsGate = false;
           
            picCanvas.Invalidate();
        }

        //Switch case later
        private void GateHasTwoConnections(int parent)
        {
            if (Gates[parent].ID == "AND")
            {
                if (Gates[parent].In1 == 1 && Gates[parent].In2 == 1)
                    Gates[parent].Value = 1;
                else if (Gates[parent].In1 == -1 || Gates[parent].In2 == -1)
                    Gates[parent].Value = -1;
                else
                    Gates[parent].Value = 0;
                if (Gates[parent].In1 == -1 || Gates[parent].In2 == -1)
                    Gates[parent].Value = -1;
            }
          
                

            if (Gates[parent].ID == "OR" )
            {

                if (Gates[parent].In1 == 0 && Gates[parent].In2 == 0)
                    Gates[parent].Value = 0;
                else if (Gates[parent].In1 == -1 || Gates[parent].In2 == -1)
                    Gates[parent].Value = -1;
                else
                    Gates[parent].Value = 1;

                if (Gates[parent].In1 == -1 || Gates[parent].In2 == -1)
                    Gates[parent].Value = -1;
            }
    

            if (Gates[parent].ID == "NOT")
            {
                if (Gates[parent].In1 == 0)
                    Gates[parent].Value = 1;
                else if (Gates[parent].In1 == -1)
                    Gates[parent].Value = -1;
                else
                    Gates[parent].Value = 0;
                if (Gates[parent].In1 == -1)
                    Gates[parent].Value = -1;
            }
           

            if (Gates[parent].ID == "NOR")
            {
                if (Gates[parent].In1 == 0 && Gates[parent].In2 == 0)
                    Gates[parent].Value = 1;
                else if (Gates[parent].In1 == -1 || Gates[parent].In2 == -1)
                    Gates[parent].Value = -1;
                else
                    Gates[parent].Value = 0;
                if (Gates[parent].In1 == -1 || Gates[parent].In2 == -1)
                    Gates[parent].Value = -1;
            }
        


            if (Gates[parent].ID == "XOR")
            {
                if (Gates[parent].In1 == 0 && Gates[parent].In2 == 0)
                    Gates[parent].Value = 0;
                else if (Gates[parent].In1 == 1 && Gates[parent].In2 == 1)
                    Gates[parent].Value = 0;
                else if (Gates[parent].In1 == -1 || Gates[parent].In2 == -1)
                    Gates[parent].Value = -1;
                else
                    Gates[parent].Value = 1;
                if (Gates[parent].In1 == -1 || Gates[parent].In2 == -1)
                    Gates[parent].Value = -1;
            }
        

            if (Gates[parent].ID == "XNOR")
            {
                if (Gates[parent].In1 == 0 && Gates[parent].In2 == 0)
                    Gates[parent].Value = 1;

                else if (Gates[parent].In1 == 1 && Gates[parent].In2 == 1)
                    Gates[parent].Value = 1;
                else if (Gates[parent].In1 == -1 || Gates[parent].In2 == -1)
                    Gates[parent].Value = -1;
                else
                    Gates[parent].Value = 0;

                if (Gates[parent].In1 == -1 || Gates[parent].In2 == -1)
                    Gates[parent].Value = -1;
            }
      

            if (Gates[parent].ID == "NAND")
            {
                if (Gates[parent].In1 == 1 && Gates[parent].In2 == 1)
                    Gates[parent].Value = 0;
                else if (Gates[parent].In1 == -1 || Gates[parent].In2 == -1)
                    Gates[parent].Value = -1;
                else
                    Gates[parent].Value = 1;
                if (Gates[parent].In1 == -1 || Gates[parent].In2 == -1)
                    Gates[parent].Value = -1;
            }

        }

        private void PopulateGatesArray(int count, string line, int Last, int size)
        {
            string[] data = line.Split(',');
            System.Console.WriteLine(line);

            Gates[Last] = new Gate(data[0]);
            Gates[Last].Point = new Point(Int32.Parse(data[1]), Int32.Parse(data[2]));
            Gates[Last].In1 = Int32.Parse(data[3]);
            Gates[Last].In2 = Int32.Parse(data[4]);
            Gates[Last].Value = Int32.Parse(data[5]);

            if(data[6].ToString() == "0")
            {
                Gates[Last].ParentGate = (Int32.Parse(data[6]));
            }
            else
            {
                Gates[Last].ParentGate = (Int32.Parse(data[6]) + Size_Of_Array);
            }
           
            Gates[Last].Out = (Int32.Parse(data[7]));
            
            return;
        }

        private void ConnectGates(int new_array)
        {
            int count = 0;
       
            for (int child = 0; child < new_array; child++)
            {
                if (Gates[child].Out != -1)
                {
                    int parent_gate = Gates[child].ParentGate;

                    if (Gates[child].Out == 1 && (Gates[parent_gate].ID == "NOT" || Gates[parent_gate].ID == "END"))
                    {
                        Pt1.Add(new Point((Gates[child].Point.X + D1), (Gates[child].Point.Y)));
                        Pt2.Add(new Point(((Gates[parent_gate].Point.X - D1)), (Gates[parent_gate].Point.Y)));
                        count++;
                    }
                    else if(Gates[child].Out == 1)
                    {
                        Pt1.Add(new Point((Gates[child].Point.X + D1), (Gates[child].Point.Y)));
                        Pt2.Add(new Point(((Gates[parent_gate].Point.X - D1)), (Gates[parent_gate].Point.Y - D0)));
                        count++;
                    }
                    else if (Gates[child].Out == 2)
                    {
                        Pt1.Add(new Point((Gates[child].Point.X + D1), (Gates[child].Point.Y)));
                        Pt2.Add(new Point((Gates[parent_gate].Point.X - D1), (Gates[parent_gate].Point.Y + D0)));
                        count++;
                    }
                }

            }
        }
     

        /* PaintHandler For the Objects on the PictureBox Canvas
           Input: Pt1 and Pt2 OR newPt1 and newPt2
           Output: A redrawn line from the list of points OR a new red line from newpt1 and pt2*/
        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            PaintCalls++;
            PaintHandlerTool.Text = "PaintHandler Calls: " + PaintCalls;
          
                // Draw gates
                for (int i = 0; i < Last; i++)
                {
                    //Draw the rectangle
                    if(Gates[i].Value == 1)
                    e.Graphics.FillRectangle(Brushes.Green, Gates[i].Point.X - D1, Gates[i].Point.Y - D1, rect_width, rect_height);
                    if(Gates[i].Value == 0)
                    e.Graphics.FillRectangle(Brushes.Red, Gates[i].Point.X - D1, Gates[i].Point.Y - D1, rect_width, rect_height);
                    if (Gates[i].Value == -1 && Gates[i].ID != "Null")
                    e.Graphics.FillRectangle(Brushes.Gray, Gates[i].Point.X - D1, Gates[i].Point.Y - D1, rect_width, rect_height);
                
                if(Gates[i].ID != "Null")
                e.Graphics.DrawString(Gates[i].ID, drawFont, drawBrush, Gates[i].Point.X - D1_Text, Gates[i].Point.Y - D2_Text);

                if (Gates[i].ID == "IN")
                {
                    Rectangle IN_out_rect_connect = new Rectangle(
                          (((Gates[i].Point.X - D1) + D3) - object_radius), ((Gates[i].Point.Y) - object_radius),
                          2 * object_radius + 1, 2 * object_radius + 1);
                       e.Graphics.FillEllipse(Brushes.White, IN_out_rect_connect);
                    e.Graphics.DrawEllipse(Pens.Black, IN_out_rect_connect);

                    Rectangle IN_Toggle_rect_connect = new Rectangle(
                       ((Gates[i].Point.X) - object_radius), ((Gates[i].Point.Y - D1) - object_radius),
                       2 * object_radius + 1, 2 * object_radius + 1);
                    e.Graphics.DrawRectangle(Pens.Black, IN_Toggle_rect_connect);
                    e.Graphics.FillRectangle(Brushes.Black, IN_Toggle_rect_connect);
                }
                else if(Gates[i].ID == "END")
                {
                    Rectangle END_in_rect_connect = new Rectangle(
                          (((Gates[i].Point.X - D1)) - object_radius), ((Gates[i].Point.Y) - object_radius),
                          2 * object_radius + 1, 2 * object_radius + 1);
                    e.Graphics.FillEllipse(Brushes.White, END_in_rect_connect);
                    e.Graphics.DrawEllipse(Pens.Black, END_in_rect_connect);
                }
                else if(Gates[i].ID == "NOT")
                {
                    Rectangle NOT_In_rect_connect = new Rectangle(
                     (((Gates[i].Point.X - D1)) - object_radius), ((Gates[i].Point.Y) - object_radius),
                     2 * object_radius + 1, 2 * object_radius + 1);
                    e.Graphics.FillEllipse(Brushes.White, NOT_In_rect_connect);
                    e.Graphics.DrawEllipse(Pens.Black, NOT_In_rect_connect);

                    Rectangle NOT_Out_rect_connect = new Rectangle(
                        (((Gates[i].Point.X - D1) + D3) - object_radius), ((Gates[i].Point.Y) - object_radius),
                        2 * object_radius + 1, 2 * object_radius + 1);
                    e.Graphics.FillEllipse(Brushes.White, NOT_Out_rect_connect);
                    e.Graphics.DrawEllipse(Pens.Black, NOT_Out_rect_connect);
                }
                else if( Gates[i].ID != "Null")
                {
                    //Draw the connectors
                    Rectangle End_rect_connect = new Rectangle(
                           (((Gates[i].Point.X) + D1) - object_radius), ((Gates[i].Point.Y) - object_radius),
                           2 * object_radius + 1, 2 * object_radius + 1);
                    e.Graphics.FillEllipse(Brushes.White, End_rect_connect);
                    e.Graphics.DrawEllipse(Pens.Black, End_rect_connect);

                    Rectangle Top_rect_connect = new Rectangle(
                        (((Gates[i].Point.X - D1)) - object_radius), (((Gates[i].Point.Y - D0)) - object_radius),
                        2 * object_radius + 1, 2 * object_radius + 1);
                    e.Graphics.FillEllipse(Brushes.White, Top_rect_connect);
                    e.Graphics.DrawEllipse(Pens.Black, Top_rect_connect);

                    Rectangle Bottom_rect_connect = new Rectangle(
                           (((Gates[i].Point.X - D1)) - object_radius), ((Gates[i].Point.Y + D0) - object_radius),
                           2 * object_radius + 1, 2 * object_radius + 1);
                    e.Graphics.FillEllipse(Brushes.White, Bottom_rect_connect);
                    e.Graphics.DrawEllipse(Pens.Black, Bottom_rect_connect);
                }
                }
             
            // Draw lines
            for(int h = 0; h < Pt1.Count; h++)
            {
                if(Pt1[h] != new Point(-1,-1))
                 e.Graphics.DrawLine(Pens.Blue, Pt1[h], Pt2[h]);
            }
               
                  

            // Draw the end points.
            for (int j = 0; j < Pt1.Count; j++) {
                    if (Pt2[j] != new Point(-1, -1))
                    {
                        Rectangle rect = new Rectangle(
                            Pt1[j].X - object_radius, Pt1[j].Y - object_radius,
                            2 * object_radius + 1, 2 * object_radius + 1);
                        e.Graphics.FillEllipse(Brushes.White, rect);
                        e.Graphics.DrawEllipse(Pens.Black, rect);
                    }
                }
                for (int k = 0; k < Pt2.Count; k++)
                {
                    if (Pt2[k] != new Point(-1, -1))
                    {
                        Rectangle rect = new Rectangle(
                            Pt2[k].X - object_radius, Pt2[k].Y - object_radius,
                            2 * object_radius + 1, 2 * object_radius + 1);
                        e.Graphics.FillEllipse(Brushes.White, rect);
                        e.Graphics.DrawEllipse(Pens.Black, rect);
                    }
                }
            
            // If there's a new segment under constructions, draw it.
            if (IsDrawing && IsLine)
            {
                DrawingObject.Text = "Drawing: Line - " + IsLine;
                e.Graphics.DrawLine(Pens.Red, NewPt1, NewPt2);
            }
            if(IsDrawing && IsGate)
            {
                DrawingObject.Text = "Drawing: Gate - " + IsGate;
                e.Graphics.DrawRectangle(Pens.Red, NewPt1.X, NewPt1.Y, rect_width, rect_height);
            }
           
        }

        private void dELETEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteLabel.Text = "Delete: true";
            Delete = true;
        }

        private void sAVEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //BuildArrayGates();
            if (circuitname.Text == "")
            {
                MessageBox.Show("Your Circuit Name Is Empty, please fill the textbox and try again", "Error No Title",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(circuitname.Text))
                {
                    for (int i = 0; i < Last; i++)
                    {
                        string line = Gates[i].ID + "," + Gates[i].Point.X.ToString() + "," + Gates[i].Point.Y.ToString() + "," + Gates[i].In1.ToString() + ","
                            + Gates[i].In2.ToString() + "," + Gates[i].Value + "," + Gates[i].ParentGate + "," + Gates[i].Out.ToString();

                        file.WriteLine(line);
                    }
                }
            }
        }

        private void rESETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gates = new Gate[50];
            Last = 0;
            Size_Of_Array = 0;
            Pt1.Clear();
            Pt2.Clear();
            picCanvas.Invalidate();
        }
    }


    }


