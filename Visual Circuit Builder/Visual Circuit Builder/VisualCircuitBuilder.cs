using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visual_Circuit_Builder
{
    // Modify the if statement to paint the box and line. Have the box and the line move together.
    //
    public partial class VCB_Form : Form
    {
        //string Img_AND = "AND.jpg";
        //string Img_NAND = "NAND.jpg";
        //string Img_OR = "OR.jpg";
        //string Img_NOR = "NOR.jpg";
        //string Img_NOT = "NOT.jpg";
        //string Img_XOR = "XOR.jpg";
        //string Img_XNOR = "XNOR.jpg";

        int PaintCalls = 0;
        //string Img_INPUT = "";

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

        //Null Point 
        private Point Point_NULL = new Point(-1, -1);

        // We're over an object if the distance squared
        // between the mouse and the object is less than this.
        private const int over_dist_squared = (object_radius * object_radius) * 10; //scope in which mouse is over an endpoint. 
        private const int over_gate_dist_squared = ((rect_height - leeway) * (rect_width - leeway)); //scope in which the mouse is over a gate. 

        // The points that make up the line segments.
        private List<Point> Pt1 = new List<Point>();
        private List<Point> Pt2 = new List<Point>();
        //private List<Point> Pt3 = new List<Point>();
        //More lists?

        // Bools for Drawing.
        private bool IsDrawing = false;
        private bool IsLine = false;
        private bool IsGate = false;
        private Point NewPt1, NewPt2;

        /// <summary>
        /// onClick Mouse Event for the TopMenuStrip
        /// </summary>
        /// <param name="sender"> refers to the object that invoked the event that fired the event handler</param>
        /// <param name="e">arguments of the event fired</param>
        private void onClick(object sender, EventArgs e)
        {
            DrawGate(sender.ToString()); 
        }

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
            int segment_number;

            //Check If the e.X and Y is near an endpoint or gate center point.
            if (MouseIsOverEndpoint(e.Location, out segment_number, out hit_point)) 
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

                // Remember the offset from the mouse to the point.
                OffsetX = hit_point.X - e.X;
                OffsetY = hit_point.Y - e.Y;
            }
            if(MouseIsOverGate(e.Location, out segment_number, out hit_point))
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

                // Remember the offset from the mouse to the point.
                OffsetX = hit_point.X - e.X;
                OffsetY = hit_point.Y - e.Y;
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
                    NewPt1 = new Point(e.X, e.Y); //First point of the line
                    NewPt2 = new Point(e.X, e.Y); //Last point of the line
                }
                //If the user choose a gate to draw
                else
                {
                    NewPt1 = new Point(e.X, e.Y); //point of the box
                    NewPt2 = new Point(-1, -1); //maintain the datastructure 
                }

            }
           
        }
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
                ///
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
            for (int i = 0; i < Pt1.Count; i++)
            {
                //Check the Lists for a matching pair.
                if (FindDistanceToGateSquared(mouse_pt, Pt1[i]) < over_gate_dist_squared) //over_gate_dist_squared the size of the gate.
                {
                    if (Pt2[i] == new Point(-1, -1)) //its a gate
                    {
                        MousePointing.Text = "MousePoint: GateEndPoint";
                        /*Check each endpoint*/
                        if (FindDistanceToPointSquared(mouse_pt, new Point((Pt1[i].X - 24), (Pt1[i].Y - 12))) < over_dist_squared)
                        {
                            hit_pt = new Point((Pt1[i].X - 24), (Pt1[i].Y - 12));
                            return true;
                        }
                        if (FindDistanceToPointSquared(mouse_pt, new Point((Pt1[i].X + 24), Pt1[i].Y)) < over_dist_squared)
                        {
                            hit_pt = new Point((Pt1[i].X + 24), Pt1[i].Y);
                            return true;
                        }
                        if (FindDistanceToPointSquared(mouse_pt, new Point((Pt1[i].X - 24), (Pt1[i].Y) + 12)) < over_dist_squared)
                        {
                            hit_pt = new Point((Pt1[i].X - 24), (Pt1[i].Y) + 12);
                            return true;
                        }
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
        // The segment we're moving or the segment whose end point we're moving.
        private int MovingSegment = -1;

        // The end point we're moving.
        private bool MovingStartPoint = false;

        // The offset from the mouse to the object being moved.
        private int OffsetX, OffsetY;

        //Menu Strip on top of the canvas//
        private void DrawGate(string gate)
        {
            if (gate.Equals("AND"))
            {
                IsGate = true;            
                GateToolStrip.Text = "GateTooStrip: AND";
            }
            else if (gate.Equals("NAND"))
            {
                IsGate = true; //pictureBox.Image = Image.FromFile(Img_NAND);
                GateToolStrip.Text = "GateToolStrip: NAND";
            }
            else if (gate.Equals("OR"))
            {
                IsGate = true;
                GateToolStrip.Text = "GateToolStrip: OR";
            }
            else if (gate.Equals("NOR"))
            {
                IsGate = true;//pictureBox.Image = Image.FromFile(Img_NOR);
                GateToolStrip.Text = "GateToolStrip: NOR";
            }
            else if (gate.Equals("NOT"))
            {
                IsGate = true; //pictureBox.Image = Image.FromFile(Img_NOT);
                GateToolStrip.Text = "GateToolStrip: NOT";
            }
            else if (gate.Equals("XOR"))
            {
                IsGate = true;//pictureBox.Image = Image.FromFile(Img_XOR);
                GateToolStrip.Text = "GateToolStrip: XOR";
            }
            else if (gate.Equals("XNOR"))
            {
                IsGate = true;//pictureBox.Image = Image.FromFile(Img_XNOR);
                GateToolStrip.Text = "GateToolStrip: XNOR";
            }
            else
            {
                IsGate = true;
                Console.WriteLine("INPUT"); //no image yet
            }

           // Invalidate();
        }

        // See if the mouse if over a box
        private bool MouseIsOverGate(Point mouse_pt, out int segment_number, out Point hit_pt)
        {
            for (int i = 0; i < Pt1.Count; i++)
            {
                // Check the starting point.
                if (FindDistanceToPointSquared(mouse_pt, Pt1[i]) < over_dist_squared) //over_dist_squared size diameter of the circle
                {
                    // We're over this point.
                    segment_number = i;
                    hit_pt = Pt1[i];
                    if (Pt2[i] == new Point(-1, -1))
                    {
                        MousePointing.Text = "MousePoint: Gate";
                        return true; //return a -1 to represent no point was found
                    }
                }
            }
            segment_number = -1;
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
                    if (Pt2[i] == new Point(-1, -1))
                    {
                        MousePointing.Text = "MousePoint: Gate";
                        segment_number = -1;
                        hit_pt = new Point(-1, -1);
                        return false; //return a -1 to represent no point was found
                    }

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
         * Output: call to invalidat
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
            else
            {
                if (MouseIsOverGateEndpoint(e.Location, out hit_pt))
                    SetMouse(hit_pt);
                Pt2[MovingSegment] =
                    new Point(e.X + OffsetX, e.Y + OffsetY);
            }

            // Redraw.
            picCanvas.Invalidate();
        }

        private void picCanvas_MouseMove_MovingGate(object sender, MouseEventArgs e)
        {


        }

        private void picCanvas_MouseUp_MovingGate(object sender, MouseEventArgs e)
        {


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
            IsNearGateEndPoint(e.Location);
            // Save the new point.
            if (IsLine)
            {
                 if (MouseIsOverGateEndpoint(e.Location, out hit_pt))
                    SetMouse(hit_pt); 
                NewPt2 = new Point(e.X, e.Y); //as the user moves the mouse update the last point
            }
            if(IsGate)
            {
                
                NewPt1 = new Point(e.X, e.Y); //as the user moves the mouse update the last point
            }
            // Redraw.
            picCanvas.Invalidate();
        }

        private void IsNearGateEndPoint(Point location)
        {
            //Fcn to snap the endpoint onto the gate. 
        }

        // Stop drawing.
        private void picCanvas_MouseUp_Drawing(object sender, MouseEventArgs e)
        {
            MouseEvent.Text = "MouseUp_Drawing";
            IsDrawing = false;
            IsLine = false;
            IsGate = false;

            // Reset the event handlers.
            picCanvas.MouseMove -= picCanvas_MouseMove_Drawing;
            picCanvas.MouseMove += picCanvas_MouseMove_NotDown;
            picCanvas.MouseUp -= picCanvas_MouseUp_Drawing;

            // Create the new segment.
            Pt1.Add(NewPt1);
            Pt2.Add(NewPt2);
            /*NOTE! - Here is were we can add the point to a datastructure.*/

            // Redraw.
            picCanvas.Invalidate();
        }

        /* PaintHandler For the Objects on the PictureBox Canvas
           Input: Pt1 and Pt2 OR newPt1 and newPt2
           Output: A redrawn line from the list of points OR a new red line from newpt1 and pt2*/
        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            PaintCalls++;
            PaintHandlerTool.Text = "PaintHandler Calls: " + PaintCalls;
            // Draw the segments.
          
                for (int i = 0; i < Pt1.Count; i++)
                {
                    // If square pt2[i] should contain -1,-1 
                    if (Pt2[i] == new Point(-1, -1))
                    {
                        //Draw the rectangle
                        e.Graphics.DrawRectangle(Pens.Red, Pt1[i].X - 24, Pt1[i].Y - 24, rect_width, rect_height);
                        
                        //Draw the connectors
                        Rectangle A_rect_connect = new Rectangle(
                               (((Pt1[i].X - 24) + 48) - object_radius), (((Pt1[i].Y - 24) + 24) - object_radius),
                               2 * object_radius + 1, 2 * object_radius + 1);
                        e.Graphics.FillEllipse(Brushes.White, A_rect_connect);
                        e.Graphics.DrawEllipse(Pens.Black, A_rect_connect);

                        Rectangle B_rect_connect = new Rectangle(
                            (((Pt1[i].X - 24)) - object_radius), (((Pt1[i].Y - 24) + 12) - object_radius),
                            2 * object_radius + 1, 2 * object_radius + 1);
                        e.Graphics.FillEllipse(Brushes.White, B_rect_connect);
                        e.Graphics.DrawEllipse(Pens.Black, B_rect_connect);

                        Rectangle C_rect_connect = new Rectangle(
                               (((Pt1[i].X - 24)) - object_radius), (((Pt1[i].Y - 24)+ 36) - object_radius),
                               2 * object_radius + 1, 2 * object_radius + 1);
                        e.Graphics.FillEllipse(Brushes.White, C_rect_connect);
                        e.Graphics.DrawEllipse(Pens.Black, C_rect_connect);

                    }
                    else
                    {
                        e.Graphics.DrawLine(Pens.Blue, Pt1[i], Pt2[i]);
                    }
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



    }

}
