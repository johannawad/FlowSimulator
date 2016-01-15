using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FlowSimulator
{
    public class Splitter : Part
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
                if (InPut != null)
                {
                    return InPut.CurrentFlow;
                }
                else
                {
                    return 0;
                }
            }

            set
            {
               
            }
        }

        /// <summary>
        /// returns a number of connected nodes
        /// </summary>
        /// <returns></returns>
        public int CheckConnectedNodes()
        {
           int  filledports=0;
            if (!ReferenceEquals(OutPutDown , null))
                {
                filledports++;
            }
                if (!ReferenceEquals(OutPutUp,null))
                {
                filledports++;
            }
                if (!ReferenceEquals(InPut,null))
                {
                filledports++;
            }
            return filledports;
        }

        /// <summary>
        /// To establish a connection
        /// </summary>
        /// <returns></returns>
        public override void Connect(Pipeline pipeline)
        {
            if (ReferenceEquals(InPut, null))
            {
                InPut = pipeline;
            }
            else if (ReferenceEquals(OutPutUp, null))
            {
                OutPutUp = pipeline;
            }
            else if (ReferenceEquals(OutPutDown, null))
            {
                OutPutDown = pipeline;
            }
        }

        

        public Splitter(Point position): base(position)
        {
            this.PercentageUp = .5;
            this.PercentageDown = .5;
            
           this.compImage = new Bitmap(Properties.Resources.splitter);
           this.compImageNot = new Bitmap(Properties.Resources.splitterNot);
           this.compIcon = new Bitmap(Properties.Resources.splitterIco);
           this.compIconNot = new Bitmap(Properties.Resources.splitterNotIco);
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

        public override void Disconnect(Pipeline p)
        {
            if (OutPutUp == p)
            {
                OutPutUp = null;
            }
            if (OutPutDown == p)
            {
                OutPutDown = null;
            }
            if (InPut == p)
            {
                InPut = null;
            }
        }

        public override Port WhichPort(Pipeline p)
        {
            if (InPut == p)
            {
                return Port.InPut;
            }
            if (OutPutUp == p)
            {
                return Port.OutPutUp;
            }
            return Port.OutPutDown;
        }
    }
}
