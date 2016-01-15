using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FlowSimulator
{
    public class Pump : Part
        
    {
       
        /// <summary>
        /// The pipeline connected to the node of this pump
        /// </summary>
        public bool PipelineConnected = false;


        /// <summary>
        /// establishes a connection
        /// </summary>
        /// <param name="pipeline"></param>
        /// <returns></returns>
        public override void Connect(Pipeline pipeline)
        {
            OutPut = pipeline;
        }

        public override void Disconnect(Pipeline p)
        {
            OutPut = null;
        }
        public Pump(Point position): base(position)
        {
            this.compImage = new Bitmap(Properties.Resources.pump);
            this.compImageNot = new Bitmap(Properties.Resources.pumpNot);
            this.compIcon = new Bitmap(Properties.Resources.pumpIco);
            this.compIconNot = new Bitmap(Properties.Resources.pumpNotIco);

        }
        public bool Input { get; set; }

        public override double CurrentFlow
        {
            get
            {
                return currentflow;
            }

            set
            {
                currentflow = value;
            }
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
            
            return Port.OutPut;
        }
    }
}
