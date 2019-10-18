[视频链接](https://v.youku.com/v_show/id_XNDM3MDMxMjAzNg==.html?spm=a2h3j.8428770.3416059.1&qq-pf-to=pcqq.c2c)

1.要求，首先 添加接口类IActionManager，用于链接场记和运动管理器，判断飞碟的回收。 

    namespace Interfaces
    {
        public interface ISceneController
        {
            void LoadResources();
        }
    
        public interface UserAction
        {
            void Hit(Vector3 pos);
            void Restart();
            int GetScore();
            bool RoundStop();
            int GetRound();
        }
    
        public enum SSActionEventType : int { Started, Completed }
    
        public interface SSActionCallback
        {
            void SSActionCallback(SSAction source);
        }
    
        public interface IActionManager
        {
            void PlayDisk(Disk disk);
            bool IsAllFinished(); 
        }
    }
2.还要写一个基于unity物理引擎的控制类 CCPhysisAction ，其实现比数学方法的模拟简单很多。

    public class CCPhysisAction : SSAction
    {
        public float speedx;
        public override void Start ()
        {
            if (!this.gameObject.GetComponent<Rigidbody>())
            {
                this.gameObject.AddComponent<Rigidbody>();
            }
            this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 9.8f * 0.6f, ForceMode.Acceleration);
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(speedx, 0, 0), ForceMode.VelocityChange);
        }
    
        private CCPhysisAction()
        {
            
        }
        public static CCPhysisAction getAction(float speedx)
        {
            CCPhysisAction action = CreateInstance<CCPhysisAction>();
            action.speedx = speedx;
            return action;
        }
    
        override public void Update ()
        {
            if (transform.position.y <= 3)
            {
                Destroy(this.gameObject.GetComponent<Rigidbody>());
                destroy = true;
                CallBack.SSActionCallback(this);
            }
        }
    }