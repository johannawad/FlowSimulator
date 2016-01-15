using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FlowSimulator
{
    public class Merger : Part
    {  
       


        /// <summary>
        /// Determines the percentage through the first channel of the splitter
        /// </summary>
        public double PercentageUp { get; set; }

        /// <summary>
        /// The remainder from the PercentageUp
        /// </summary>
        public double PercentageDown { get; set; }

        public override double CurrentFlow
        {
            get
            {
                double outflow = 0;
                if (InPutUp != null)
                {
                    outflow += InPutUp.CurrentFlow;
                }
                if (InPutDown != null)
                {
                    outflow += InPutDown.CurrentFlow;
                }
                return outflow;
            }

            set
            {
                
            }
        }


        /// <summary>
        /// To establish a connection with a pipeline
        /// </summary>
        /// <param name="edge"></param>
        public override void Connect(Pipeline pipeline) 
        {
            if (ReferenceEquals(InPutUp, null))
            {
                InPutUp = pipeline;
            }
            else if(ReferenceEquals(InPutDown, null))
            {
                InPutDown = pipeline;
            }
            else if(ReferenceEquals(OutPut, null))
            {
                OutPut = pipeline;
            }
        }
        public override void Disconnect(Pipeline p)
        {
            if (InPutUp == p)
            {
                InPutUp = null;
            }
            if (InPutDown == p)
            {
                InPutDown = null;
            }
            if (OutPut == p)
            {
                OutPut = null;
            }
        }
        /// <summary>
        /// returns how many nodes have been connected with a pipeline
        /// </summary>
        /// <returns></returns>
        public int CheckConnectedNodes()
        {

            int filledports = 0;
            if (!ReferenceEquals(OutPut,null))
            {
                filledports++;
            }
            if (!ReferenceEquals(InPutDown,null))
            {
                filledports++;
            }
            if (!ReferenceEquals(InPutUp,null))
            {
                filledports++;
            }
            return filledports;
        }
        public Merger(Point position): base(position)
        {
            this.compImage = new Bitmap(Properties.Resources.merger);
            this.compImageNot = new Bitmap(Properties.Resources.mergerNot);
            this.compIcon = new Bitmap(Properties.Resources.mergerIco);
            this.compIconNot = new Bitmap(Properties.Resources.mergerNotIco);
        }
        public override Image ComponentImage(bool isNotOccupied)
        {
            if (isNotOccupied)
            {
                return compImage;
            }
            else return compImageNot;
        }
        public override Image ComponentIconImage(bool isNotOccupied)
        {
            if (isNotOccupied)
            {
                return compIcon;
            }
            else return compIconNot;
        }

        public override Port WhichPort(Pipeline p)
        {
            if (InPutUp == p)
            {
                return Port.InPutUp;
            }
            if (InPutDown == p)
            {
                return Port.InPutDown;
            }
            return Port.OutPut;
        }
    }
}
