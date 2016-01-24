using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowSimulator.Properties;
namespace FlowSimulator
{
    [Serializable()]
    public class Pipeline : Component
    {

        private double safetylimit;
        /// <summary>
        /// if the currentflow exceeds the safetylimit the pipe will be red
        /// </summary>
        public double SafetyLimit { get { return safetylimit; } set { if (value < 0) { safetylimit = 0; } else { safetylimit = value; } } }

        /// <summary>
        /// Determines if the CurrentFlow through the pipeline is critical
        /// </summary>
        public bool IsCritical
        {
            get
            {
                if (CurrentFlow > SafetyLimit)
                    return true;
                else
                {
                    return false;
                }
            }
        }

        public Pipeline(Component InPut, Component OutPut)
        {
            this.InPut = InPut;
            this.OutPut = OutPut;
            SafetyLimit = 5;

        }

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
                        switch (((Part)InPut).WhichPort(this))
                        {
                            case Part.Port.InPutUp:
                                return new Point(InPut.Position.X, InPut.Position.Y + 10);

                            case Part.Port.InPutDown:
                                return new Point(InPut.Position.X, InPut.Position.Y + 30);

                            case Part.Port.OutPut:
                                return new Point(InPut.Position.X + 40, InPut.Position.Y + 20);
                        }
                        break;
                    case ComponentType.Sink:
                        return new Point(InPut.Position.X, InPut.Position.Y + 17);

                }
                return new Point(0, 0);

            }
        }
        /// <summary>
        /// The end point of the connection
        /// </summary>
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
        /// <summary>
        /// it gets the CurrentFlow from the input Component
        /// </summary>
        public override double CurrentFlow
        {

            get
            {
                try
                {
                    switch (((Part)InPut).WhichPort(this))
                    {

                        case Part.Port.OutPut:
                            return InPut.CurrentFlow;

                        case Part.Port.OutPutUp:
                            return (((Splitter)InPut).PercentageUp * InPut.CurrentFlow);
                        case Part.Port.OutPutDown:
                            return (((Splitter)InPut).PercentageDown * InPut.CurrentFlow);
                        default:
                            return 0;
                    }
                }
                catch (Exception)
                {

                    return 0;
                }
            }
            set
            {
                if (InPut != null)
                { currentflow = InPut.CurrentFlow; }
                else
                {

                    currentflow = 0;
                }

            }
        }
    }
}

