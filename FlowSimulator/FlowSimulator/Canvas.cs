using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using FlowSimulator.Properties;
using System.Diagnostics;

namespace FlowSimulator
{
    /// <summary>
    /// This class contains the methods for the drawing area(canvas) and when a new component is drawn, it will be added to a list in this class.
    /// </summary>
    ///
    public class Canvas
    {
       

        /// <summary>
        /// This list will contain all the components that have been drawn on the canvas
        /// </summary>
        public List<Component> Components
        {
            get;
            set;
        }
        /// <summary>
        /// To obtain the coordinates of the position that has been clicked by the mouse
        public Point MousePoint
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        public Size Size { get; set; }

        /// <summary>
        /// Refers to a component that has been selected on the canvas
        /// </summary>
        public Component SelectedComponent
        {
            get;
            set;
        }

        /// <summary>
        /// When multiple components on the canvas have been selected
        /// </summary>
        public List<Component> SelectedComponents
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// To store an action eg. save, undo etc.
        /// </summary>
        public Action Action
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// This list contains the undo-redo actions
        /// </summary>
        public List<Action> UndoRedoList = new List<Action>();
        /// <summary>
        /// this int is used for knowing where canvas is at while using the Undo-Redo List
        /// </summary>
        public int UndoRedoIndex = -1;


        /// <summary>
        /// Responsible for saving a canvas and loading existing canvas
        /// </summary>
        public FileHelper Filehelper
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// To add a new component to the canvas
        /// </summary>
        /// <param name="mousePoint"></param>
        /// <param name="selectedCompIndex"></param>
        public void CreateComponent(Component c)
        {
            Components.Add(c);
        }

        /// <summary>
        /// To remove an existing component from the canvas
        /// </summary>
        /// <param name="selectedComponent"></param>
        

        /// <summary>
        /// To draw a rectangle around the selected component
        /// </summary>
        /// <param name="g"></param>
        public void DrawSelection(PaintEventArgs e, Component selectedComponent)
        {
            foreach (Component c in Components)
            {
                if (c == selectedComponent)
                {
                    Pen p = new Pen(System.Drawing.Color.Gray, 2);
                    e.Graphics.DrawRectangle(p, c.Position.X, c.Position.Y, 40, 40);
                }
            }

        }

        /// <summary>
        /// To select a component on the canvas
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Component SelectComponent(Point point)
        {
            //Point p = point;
            Rectangle temp = new Rectangle(point.X - 5, point.Y - 5, 10, 10);
            foreach (Component c in Components)
            {
                if (c is Pipeline)
                {
                   Pipeline pipeline = ((Pipeline)c);
                    if (DistanceFromPointToLine(point, pipeline.InPutPoint, pipeline.OutPutPoint) < 4)
                    {
                        return c;
                    }
                }
                //if (c.SelectionArea.Contains(point))
                if (temp.IntersectsWith(c.SelectionArea))
                {
                    return c;
                }
            }
            return null;

        }

        /// <summary>
        /// To check if it is overlapping
        /// </summary>
        /// <param name="selectedComponent"></param>

        public bool IsOverlapping(Rectangle newPlace)
        {

            foreach (Component c in Components)
            {
                Point p = c.Position;
                Rectangle rec = c.SelectionArea;
                if (newPlace.IntersectsWith(c.SelectionArea))
                {

                    return true;

                }

            }
            return false;
        }

        public Point getMidPoint(Pipeline p)
        {
            return new Point((p.InPutPoint.X + p.OutPutPoint.X) / 2, (p.InPutPoint.Y + p.OutPutPoint.Y) / 2);
        }


        /// <summary>
        /// To adjust the properties of a component on the canvas
        /// </summary>
        /// <param name="selectedComponent"></param>
        /// <returns></returns>
        public bool AdjustProperties(Component selectedComponent)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// To remove multiple selected components from the canvas
        /// </summary>
        /// <param name="selectedComponents"></param>
        public void DeleteComponent(Component wantedcomp)
        {
            //Rectangle tempMousePoint = new Rectangle(mouseClicked, new Size(1, 1));

           
            // removes all Components connected
            switch (wantedcomp.GetType())
            {
                
                case ComponentType.Pump:
                    if (wantedcomp.OutPut!=null)
                    {
                        DeAttachPipeLine((Pipeline)wantedcomp.OutPut);
                    }
                    
                    break;
                case ComponentType.Splitter:
                    if (wantedcomp.InPut != null)
                    {
                        DeAttachPipeLine((Pipeline)wantedcomp.InPut);
                    }
                    if (wantedcomp.OutPutUp != null)
                    {
                        DeAttachPipeLine((Pipeline)wantedcomp.OutPutUp);
                    }
                    if (wantedcomp.OutPutDown != null)
                    {
                        DeAttachPipeLine((Pipeline)wantedcomp.OutPutDown);
                    }
                    break;
                case ComponentType.Merger:
                    if (wantedcomp.InPutUp != null)
                    {
                        DeAttachPipeLine((Pipeline)wantedcomp.InPutUp);
                    }
                    if (wantedcomp.InPutDown != null)
                    {
                        DeAttachPipeLine((Pipeline)wantedcomp.InPutUp);
                    }
                    if (wantedcomp.OutPut != null)
                    {
                        DeAttachPipeLine((Pipeline)wantedcomp.OutPut);
                    }
                    break;
                case ComponentType.Sink:
                    if (wantedcomp.InPut != null)
                    {
                        DeAttachPipeLine((Pipeline)wantedcomp.InPut);
                    }
                    break;
                
            }
            Components.Remove(wantedcomp);
        }

        public Pipeline findSplitterPipeline(Component InPut)
        {
            Pipeline pipeline = null;
            foreach (Component comp in Components)
            {
                if (comp is Pipeline)
                {
                    pipeline = ((Pipeline)comp);

                    if (pipeline != null)
                    {
                        if (pipeline.InPut == InPut)
                            return SelectPipeline(new Point(InPut.Position.X + 40, InPut.Position.Y + 10));
                        // temp = pipeline;
                    }
                }
            }
            return pipeline;
        }

        /// <summary>
        /// To calculate the maximum flow through the network
        /// </summary>
        /// <returns></returns>
        /// 
        


        /// <summary>
        /// To calculate the maximum flow through the network
        /// </summary>
        /// <returns></returns>


        /// <summary>
        /// To draw a created component
        /// </summary>
        /// <param name="component"></param>
        /// <param name="e"></param>
        public void DrawComponent(Component c, PaintEventArgs e)
        {
            Image pump = Properties.Resources.pump;
            e.Graphics.DrawImage(pump, c.Position);

        }


        /// <summary>
        /// To undo the last action eg. adding a component
        /// </summary>
        /// <summary>
        /// To undo the last action eg. adding a component
        /// </summary>
        public void UndoLastAction()
        {
            Action CurrentAction = UndoRedoList[UndoRedoIndex];
            switch (CurrentAction.ActionType)
            {
                case ActionType.Create:
                    UndoRedoList[UndoRedoIndex] = new Action(ActionType.Delete, CurrentAction.Component);
                     if (CurrentAction.Component is Pipeline) 
                     DeletePipeline((Pipeline)CurrentAction.Component); 
                     else 
                     DeleteComponent(CurrentAction.Component);
                    
                   
                    break;
                case ActionType.Move:
                    UndoRedoList[UndoRedoIndex] = new Action(ActionType.Move, CurrentAction.Component);
                    CurrentAction.Component.Position = CurrentAction.Position;
                    break;
                case ActionType.Delete:
                    ReAddComponent(CurrentAction.Component);
                    UndoRedoList[UndoRedoIndex] = new Action(ActionType.Create, CurrentAction.Component);
                    break;

            }
            UndoRedoIndex--;
        }

        /// <summary>
        /// To redo the last 'undo' action
        /// </summary>
        public void RedoLastAction()
        {
            Action RedoAction = UndoRedoList[UndoRedoIndex + 1];
            switch (RedoAction.ActionType)
            {
                case ActionType.Create:
                    UndoRedoList[UndoRedoIndex + 1] = new Action(ActionType.Delete, RedoAction.Component);
                    if (RedoAction.Component is Pipeline)
                     DeletePipeline((Pipeline)RedoAction.Component);
                     else
                     DeleteComponent(RedoAction.Component);
                    
                    break;
                case ActionType.Move:
                    UndoRedoList[UndoRedoIndex + 1] = new Action(ActionType.Move, RedoAction.Component);
                    RedoAction.Component.Position = RedoAction.Position;
                    break;
                case ActionType.Delete:
                    
                    ReAddComponent(RedoAction.Component);
                    UndoRedoList[UndoRedoIndex + 1] = new Action(ActionType.Create, RedoAction.Component);
                    break;

            }
            UndoRedoIndex++;
        }
        /// <summary>
        /// this is used for creating an undo. NOTE: use this method before you give the component it's new position or before you delete the component!
        /// </summary> 
        /// <param name="ActType">type "Create", "Delete" or "Move" here without quotations</param>
        /// <param name="comp">reference to the component</param>
        public void CreateUndo(ActionType ActType, Component comp)
        {
            if (UndoRedoIndex + 1 < UndoRedoList.Count)
            { UndoRedoList.RemoveAt(UndoRedoIndex + 1); }
            UndoRedoList.Add(new Action(ActType, comp));
            UndoRedoIndex++;
           

        }


        /// <summary>
        /// To redraw an action that was deleted or moved(part of the Undo/redo methods)
        /// </summary>
        /// <param name="action"></param>
        public void ReDraw(Action action)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// To draw a created component that is a pipeline
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        public Pipeline SelectPipeline(Point p)
        {
            Pipeline pipe;
            foreach (Component comp in Components)
            {
                if (comp is Pipeline)
                {
                    pipe = ((Pipeline)comp);
                    if (DistanceFromPointToLine(p, pipe.InPutPoint, pipe.OutPutPoint) < 4)
                    {
                        return pipe;
                    }
                }
            }
            return null;
        }
        public static double DistanceFromPointToLine(Point pt, Point p1, Point p2)
        {
            PointF closest;
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;

            // Calculate the t that minimizes the distance.
            float t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) / (dx * dx + dy * dy);

            // See if this represents one of the segment's
            // end points or a point in the middle.
            if (t < 0)
            {
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
            }
            else if (t > 1)
            {
                dx = pt.X - p2.X;
                dy = pt.Y - p2.Y;
            }
            else
            {
                closest = new PointF(p1.X + t * dx, p1.Y + t * dy);
                dx = pt.X - closest.X;
                dy = pt.Y - closest.Y;
            }
            return Math.Sqrt(dx * dx + dy * dy);
        }
        /// <summary>
        /// A simple method that checkes wich Component you are looking for and returns the one you are looking for
        /// </summary>
        /// <param name="p">the coordinates</param>
        /// <returns></returns>
        /* public Component GetComponent(Point p)
         {


             Rectangle temp = new Rectangle(p.X - 5, p.Y - 5, 10, 10);
             foreach (Component c in Components)
             {
                 if (c is Pipeline)
                 {
                     Pipeline pipeline = ((Pipeline)c);
                     if (DistanceFromPointToLine(p, pipeline.InPutPoint, pipeline.OutPutPoint) < 4)
                     {
                         return c;
                     }
                 }
                 //if (c.SelectionArea.Contains(point))
                 if (temp.IntersectsWith(c.SelectionArea))
                 {
                     return c;
                 }
             }
             return null;




         }
         */


        /// <summary>
        /// This method is only used for undo and redo as it needs to keep a connection to the input component
        /// </summary>
        /// <param name="pipe">the selected pipe</param>   
        public void DeAttachPipeLine(Pipeline pipe)
        {
            // delete connections

            // ((Part)pipe.InPut).Disconnect(pipe);

            ((Part)pipe.OutPut).Disconnect(pipe);

            Components.Remove(pipe);           //delete selected wire
        }
        public void DeletePipeline(Pipeline pipe)
        {
            
                // delete connections
               
                   ((Part)pipe.InPut).Disconnect(pipe);
       
                    ((Part)pipe.OutPut).Disconnect(pipe);
           
            Components.Remove(pipe);           //delete selected wire
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comp"></param>
        public void ReAddComponent(Component comp)
        {
            Components.Add(comp);
            switch (comp.GetType())
            {
                case ComponentType.Pipeline:
                    if (comp.InPut != null)
                    {
                        ((Part)(comp).InPut).Connect((Pipeline)comp);
                    }

                    if (comp.OutPut != null)
                    {
                        ((Part)(comp).OutPut).Connect((Pipeline)comp);
                    }
                    break;
                case ComponentType.Pump:
                    if (comp.OutPut != null)
                    { ((Part)(comp).OutPut.OutPut).Connect((Pipeline)comp.OutPut);
                        Components.Add(comp.OutPut);
                    }

                    break;
                case ComponentType.Splitter:
                    if (comp.InPut != null)
                    { ((Part)(comp).InPut.OutPut).Connect((Pipeline)comp.InPut);
                        Components.Add(comp.InPut);
                    }
                    if (comp.OutPutUp != null)
                    { ((Part)(comp).OutPutUp.OutPut).Connect((Pipeline)comp.OutPutUp);
                        Components.Add(comp.OutPutUp);
                    }
                    if (comp.OutPutDown != null)
                    { ((Part)(comp).OutPutDown.OutPut).Connect((Pipeline)comp.OutPutDown);
                        Components.Add(comp.OutPutDown);
                    }
                    break;
                case ComponentType.Merger:
                    if (comp.OutPut != null)
                    { ((Part)(comp).OutPut.OutPut).Connect((Pipeline)comp.OutPut);
                        Components.Add(comp.OutPut);
                    }
                    if (comp.InPutUp != null)
                    { ((Part)(comp).InPutUp.OutPut).Connect((Pipeline)comp.InPutUp);
                        Components.Add(comp.InPutUp);
                    }
                    if (comp.InPutDown != null)
                    { ((Part)(comp).InPutDown.OutPut).Connect((Pipeline)comp.InPutDown);
                        Components.Add(comp.InPutDown);
                    }
                    break;
                case ComponentType.Sink:
                    if (comp.InPut != null)
                    {
                        ((Part)(comp).InPut.OutPut).Connect((Pipeline)comp.InPut);
                        Components.Add(comp.InPut);
                    }
                    break;
                
            }
           
        }
        
     
        /*
        private bool isUpdated = false;
        public void UpdateProperties(Component updatedComponent)
        {
            Component temp = null;
            Pipeline pipeline = null;
            foreach (Component comp in Components)
            {
                if (comp is Pipeline)
                {
                    pipeline = ((Pipeline)comp);
                }
                if (pipeline != null)
                {
                    if (pipeline.InPut == updatedComponent)
                    {
                        if (!isUpdated)
                        {
                            pipeline.OutPut.CurrentFlow += (updatedComponent.CurrentFlow - pipeline.CurrentFlow);
                            pipeline.CurrentFlow = pipeline.OutPut.CurrentFlow;

                        }
                        if (pipeline.CurrentFlow != pipeline.InPut.CurrentFlow && pipeline.InPut.GetType() == typeof(Pump))
                        {
                            pipeline.CurrentFlow = pipeline.InPut.CurrentFlow;
                            pipeline.OutPut.CurrentFlow = pipeline.CurrentFlow;
                        }


                        if (pipeline.OutPut.GetType() == typeof(Splitter))
                        {
                            if (pipeline.OutPut.OutPutUp != null)
                            {

                                p = findSplitterPipeline(pipeline.OutPut.OutPutUp);
                                p.CurrentFlow = Math.Round(pipeline.OutPut.CurrentFlow *
                                                    ((Splitter)pipeline.OutPut).PercentageUp, 2);


                            }
                            if (pipeline.OutPut.OutPutDown != null)
                            {

                                p = findSplitterPipeline(pipeline.OutPut.OutPutDown);
                                p.CurrentFlow = Math.Round(pipeline.OutPut.CurrentFlow *
                                                    ((Splitter)pipeline.OutPut).PercentageDown, 2);

                            }


                            updatedComponent = pipeline.OutPut;
                        }
                    }
                }
            }
            isUpdated = true;
        }*/
        public bool DrawPipeline(Component c1, Component c2)
        {

            try
            {

            
            if(c1 == c2)
            return false;
            switch (c1.GetType())
            {
                case ComponentType.Pump:
                    if (c1.OutPut != null )
                    {
                        return false;
                    }
                    break;

                case ComponentType.Splitter:
                    if ( ((Splitter)c1).CheckConnectedNodes()  == 3  )
                    {
                        return false;
                    }
                    break;
                case ComponentType.Merger:
                    if (((Merger)c1).CheckConnectedNodes() == 3)
                    { return false; }
                        break;
                case ComponentType.Sink:
                    if (c1.InPut != null)
                    {
                        return false;
                    }
                    break;
            }
            switch (c2.GetType())
            {
                case ComponentType.Pump:
                    if (c2.OutPut != null)
                    {return false;}
                    break;
                case ComponentType.Splitter:
                    if (((Splitter)c2).CheckConnectedNodes() == 3)
                    {return false; }
                    break;
                case ComponentType.Merger:
                    if (((Merger)c2).CheckConnectedNodes() == 3)
                    { return false; }
                    break;
                case ComponentType.Sink:
                    if (c2.InPut != null)
                    { return false;}
                    break;
            }
            Pipeline pipe = new Pipeline(c1, c2);
            Components.Add(pipe);
            ((Part)c1).Connect(pipe);
            ((Part)c2).Connect(pipe);
                CreateUndo(ActionType.Create, pipe);
            return true;
            }
            catch (Exception)
            {
                return false;
                
            }
        }


        /// <summary>
        /// to give the coordinates of the line
        /// </summary>
        /// <returns></returns>
        //private Point  AssignFirstCoordinate()
        // {
        //    if()
        // }

        /// <summary>
        /// Refreshes the drawing area with all changes that has been made
        /// </summary>
        public void RefreshCanvas()
        {
            throw new System.NotImplementedException();
        }
        public Canvas()
        {

            Components = new List<Component>();


        }
       
    }
}
