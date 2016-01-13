using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FlowSimulator
{
    public class Splitter : Part
    {
        private Image compImage, compImageNot, compIcon, compIconNot;
       
        /// <summary>
        /// Determines the percentage through the first channel of the splitter
        /// </summary>
        public double PercentageUp { get; set; }

        /// <summary>
        /// The remainder from the PercentageUp
        /// </summary>
        public double PercentageDown { get; set; }

        /// <summary>
        /// returns a number of connected nodes
        /// </summary>
        /// <returns></returns>
        public int CheckConnectedNodes()
        {
            



                if (!ReferenceEquals(OutPutDown , null))
                {
                    return 3;
                }
                if (!ReferenceEquals(OutPutUp,null))
                {
                    return 2;
                }
                if (!ReferenceEquals(InPut,null))
                {
                    return 1;
                }
            return 0;
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
