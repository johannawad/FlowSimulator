using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowSimulator.Properties;
namespace FlowSimulator
{

    public class Pipeline :Component
    {
        
        /// <summary>
        /// The InPut component connected to the pipeline
        /// </summary>
      

      

      
    /// <summary>
        /// The OutPut component connected to the pipeline
        /// </summary>
       

        /// <summary>
        /// Determines if the CurrentFlow through the pipeline is critical
        /// </summary>
        public bool IsCritical
        {
            get
            {
                if (CurrentFlow > Capacity)
                    return true;
                else
                {
                    return false;
                }
            }
        }

        
        public int _selectedOutPut { get; set; }
        public int _selectedOutPut2 { get; set; }
        public Pipeline(Component InPut, Component OutPut)
        {
            this.InPut = InPut;
            this.OutPut = OutPut;
            this.Capacity = 10;
           /* if (InPut.GetType() == typeof(Splitter))
            {
                if (InPut.OutPutUp != null && InPut.OutPutDown == null)
                {
                    this.CurrentFlow = InPut.OutPutUp.CurrentFlow;
                }
                else this.CurrentFlow = InPut.OutPutDown.CurrentFlow;
            }
            else
            { this.CurrentFlow = InPut.CurrentFlow; }
            */
        }
      /*  public Pipeline(Component c1, Component c2, int selectedOutPut)
        {
            this.InPut = c1;
            this.OutPut = c2;
            this.Capacity = 10;
            _selectedOutPut = selectedOutPut;
           
            if (c1.GetType() == typeof(Splitter))
            {
                if (c1.OutPutUp != null && c1.OutPutDown == null)
                {
                    this.CurrentFlow = c1.OutPutUp.CurrentFlow;
                }
                else this.CurrentFlow = c1.OutPutDown.CurrentFlow;
            }
            else
            { this.CurrentFlow = c1.CurrentFlow; }
        }
        public Pipeline(Component c1, Component c2, int selectedOutPut, int selectedOutPut2)
        {
            this.InPut = c1;
            this.OutPut = c2;
            this.Capacity = 10;
            _selectedOutPut = selectedOutPut;
            _selectedOutPut2 = selectedOutPut2;
           
            if (c1.GetType() == typeof(Splitter))
            {
                if (c1.OutPutUp != null && c1.OutPutDown == null)
                {
                    this.CurrentFlow = c1.OutPutUp.CurrentFlow;
                }
                else this.CurrentFlow = c1.OutPutDown.CurrentFlow;
            }
            else
            { this.CurrentFlow = c1.CurrentFlow; }
            
          //  this.AssignInPutPoint();
           // this.AssignOutPutPoint();
        }
        */
        /// <summary>
        /// first point of the connection
        /// </summary>
        public Point InPutPoint
        {
            get
            {

                switch (InPut.GetType())
                {

                    case ComponentType.Pump:
                        return new Point(InPut.Position.X + 40, InPut.Position.Y + 10);

                    case ComponentType.Splitter:
                        switch (((Part)InPut).WhichPort(this))
                        {
                            case Part.Port.OutPutUp:
                                return new Point(InPut.Position.X + 40, InPut.Position.Y + 10);

                            case Part.Port.OutPutDown:
                                return new Point(InPut.Position.X + 40, InPut.Position.Y + 30);
                        }
                        break;
                    case ComponentType.Merger:
                        return new Point(InPut.Position.X + 40, InPut.Position.Y + 20);
                    case ComponentType.Sink:
                        return new Point(InPut.Position.X, InPut.Position.Y + 17);
                    
                }
                return new Point(0, 0);

            }
        }
        public Point OutPutPoint
        {
            get
            {


                switch (OutPut.GetType())
                {

                    case ComponentType.Pump:
                        return new Point(OutPut.Position.X + 40, OutPut.Position.Y + 10);

                    case ComponentType.Splitter:
                        return new Point(OutPut.Position.X, OutPut.Position.Y + 20);
                    case ComponentType.Merger:
                        switch (((Part)OutPut).WhichPort(this))
                        {
                            case Part.Port.InPutUp:
                                return new Point(OutPut.Position.X, OutPut.Position.Y + 10);

                            case Part.Port.InPutDown:
                                return new Point(OutPut.Position.X, OutPut.Position.Y + 30);
                        }
                        break;
                    case ComponentType.Sink:
                        return new Point(OutPut.Position.X, OutPut.Position.Y + 17);

                }
                return new Point(0, 0);



            } 
        }

        



    }
}
