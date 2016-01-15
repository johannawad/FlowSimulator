using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FlowSimulator
{
    public class Sink : Part
    {
        
        /// <summary>
        /// The pipeline connected to the node of this pump
        /// </summary>
        public bool PipelineConnected = false;

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

             set { }
            
        }

        /// <summary>
        /// establishes a connection
        /// </summary>
        /// <param name="pipeline"></param>
        /// <returns></returns>
        public override void Connect(Pipeline pipeline)
        {
            InPut = pipeline;
        }

        public override void Disconnect(Pipeline p)
        {
            InPut = null;
        }

        public Sink(Point position): base(position)
        {
            this.compImage = new Bitmap(Properties.Resources.sink);
            this.compImageNot = new Bitmap(Properties.Resources.sinkNot);
            this.compIcon = new Bitmap(Properties.Resources.sinkIco);
            this.compIconNot = new Bitmap(Properties.Resources.sinkNotIco);
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
            return Port.InPut;
        }
    }
}
