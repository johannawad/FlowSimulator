using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FlowSimulator
{
    [Serializable()]
    public class Merger : Part
    {
        /// <summary>
        /// it gets the CurrentFlow from the input Component
        /// </summary>
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
        /// To establish a connection to a free input or output, in the order of first input then output
        /// </summary>
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
        /// <summary>
        /// Remove an existing connection
        /// </summary>
        /// <param name="p"></param>
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
        /// <summary>
        /// returns the image to be displayed on the canvas
        /// </summary>
        /// <param name="isNotOccupied"></param>
        /// <returns></returns>
        public override Image ComponentImage(bool isNotOccupied)
        {
            if (isNotOccupied)
            {
                return compImage;
            }
            else return compImageNot;
        }
        /// <summary>
        /// returns an image if placing of the merger is restricted
        /// </summary>
        /// <param name="isNotOccupied"></param>
        /// <returns></returns>
        public override Image ComponentIconImage(bool isNotOccupied)
        {
            if (isNotOccupied)
            {
                return compIcon;
            }
            else return compIconNot;
        }
        
        /// <summary>
        /// returns the port which has been connected
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
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
