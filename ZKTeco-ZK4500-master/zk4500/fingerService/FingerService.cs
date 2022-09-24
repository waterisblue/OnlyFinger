using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxZKFPEngXControl;

namespace zk4500.fingerService
{
    class FingerService
    {
        AxZKFPEngX ZkFprint = new AxZKFPEngX();

        public FingerService()
        {

        }
        public void init()
        {
            if(ZkFprint.InitEngine() == 0)
            {
                ZkFprint.FPEngineVersion = "9";
                ZkFprint.EnrollCount = 3;
            }
        }

        public AxZKFPEngX getZkFprint()
        {
            return ZkFprint;
        }

        public void onImageReceived(object sender, IZKFPEngXEvents_OnImageReceivedEvent e)
        {
            
        }

        public void onFeatureInfo()
        {

        }

        public void onEnroll()
        {

        }
    }
}
