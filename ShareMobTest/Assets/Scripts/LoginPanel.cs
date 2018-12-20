
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using cn.sharesdk.unity3d;
using LitJson;
using System.IO;

namespace PJW.Book.UI
{
    /// <summary>
    /// 登录界面
    /// </summary>
    public class LoginPanel : MonoBehaviour
    {
        /// <summary>
        /// 退出按钮
        /// </summary>
        private Button exitBtn;
        /// <summary>
        /// QQ按钮
        /// </summary>
        private Button QQBtn;
        /// <summary>
        /// 微信按钮
        /// </summary>
        private Button weixinBtn;
        /// <summary>
        /// 微博按钮
        /// </summary>
        private Button weiboBtn;
        /// <summary>
        /// 分享按钮
        /// </summary>
        private Button ShareBtn;
        /// <summary>
        /// 用户名输入框
        /// </summary>
        private InputField userName;
        /// <summary>
        /// 密码输入框
        /// </summary>
        private InputField passWord;
        /// <summary>
        /// SDK
        /// </summary>
        public ShareSDK ssdk;
        public Image image;
        /// <summary>
        /// 控制台
        /// </summary>
        public Text text;
        /// <summary>
        /// 获取到的用户信息保存本地的文件名
        /// </summary>
        private string fileName;
        /// <summary>
        /// 获取到用户的头像保存到本地的文件名
        /// </summary>
        private string iconName;

        public  void Start()
        {
            Debug.Log(ssdk);
            ssdk.authHandler = AuthHandler;
            userName = transform.Find("UserName").GetComponentInChildren<InputField>();
            passWord = transform.Find("PassWord").GetComponentInChildren<InputField>();
            exitBtn = transform.Find("ExitBtn").GetComponent<Button>();
            QQBtn = transform.Find("QQBtn").GetComponent<Button>();
            weixinBtn = transform.Find("WeiXinBtn").GetComponent<Button>();
            weiboBtn = transform.Find("WeiBoBtn").GetComponent<Button>();
            ShareBtn = transform.Find("QQShareBtn").GetComponent<Button>();

            exitBtn.onClick.AddListener(ExitButtonHandle);
            QQBtn.onClick.AddListener(QQButtonHandle);
            weixinBtn.onClick.AddListener(WeiXinButtonHandle);
            weiboBtn.onClick.AddListener(WeiBoButtonHandle);
            ShareBtn.onClick.AddListener(ShareButtonHandle);
        }
        /// <summary>
        /// 分享
        /// </summary>
        private void ShareButtonHandle()
        {
            ShareContent content = new ShareContent();
            if (iconName != null)
                content.SetImagePath(Application.persistentDataPath + iconName);
            content.SetTitle(" GitHub分享 ");
            content.SetTitleUrl("https://github.com/Iamdevelope");
            content.SetText(" wecome my github ");
            content.SetUrl("https://www.cnblogs.com/mufei/");
            content.SetComment("test description");
            content.SetMusicUrl("http://fjdx.sc.chinaz.com/Files/DownLoad/sound1/201807/10300.mp3");
            content.SetShareType(ContentType.Webpage);
            
            Debug.Log(" ******* 001 ");
            ssdk.shareHandler = ShareHandler;
            //ssdk.ShareContent(new PlatformType[] { PlatformType.QQ,PlatformType.QZone,PlatformType.WeChat,PlatformType.WechatPlatform}, content);
            ssdk.ShowPlatformList(null, content);
            //ssdk.ShowShareContentEditor(PlatformType.WeChat, content);
            //ssdk.ShowPlatformList(null, content, 100, 100);
        }
        /// <summary>
        /// 退出
        /// </summary>
        private void ExitButtonHandle()
        {
            Application.Quit();
        }
        /// <summary>
        /// QQ登录
        /// </summary>
        private void QQButtonHandle()
        {
            Debug.Log("点击了QQ登录");
            fileName = "/qq.json";
            iconName = "/qqIcon.jpg";
            Authorize(PlatformType.QQ);
        }
        /// <summary>
        /// 微博登录
        /// </summary>
        private void WeiBoButtonHandle()
        {
            Debug.Log("点击了微博登录");
            fileName = "/sina.json";
            iconName = "/sinaIcon.jpg";
            Authorize(PlatformType.SinaWeibo);
        }
        /// <summary>
        /// 微信登录
        /// </summary>
        private void WeiXinButtonHandle()
        {
            Debug.Log("点击了微信登录");
            fileName = "/wechat.json";
            iconName = "/wechatIcon.jpg";
            Authorize(PlatformType.WeChat);
        }
        private void Authorize(PlatformType platform)
        {
            if (File.Exists(Application.persistentDataPath + fileName))
            {
                text.text += "\n 已经授权了，可以直接登录.";
                return;
            }
            ssdk.Authorize(platform);
        }
        /// <summary>
        /// 分享回调
        /// </summary>
        /// <param name="reqID"></param>
        /// <param name="state"></param>
        /// <param name="type"></param>
        /// <param name="data"></param>
        private void ShareHandler(int reqID, ResponseState state, PlatformType type, Hashtable data)
        {
            if (state == ResponseState.Success)
            {
                text.text += "\n share is success and the hashtable is " + JsonMapper.ToJson(data);
                Debug.Log(" share is success ");
                Debug.Log(JsonMapper.ToJson(data));
            }
            else if(state==ResponseState.Fail)
            {
                Debug.Log(" share is fail ");
            }
        }
        /// <summary>
        /// 获取到用户信息回调
        /// </summary>
        /// <param name="reqID"></param>
        /// <param name="state"></param>
        /// <param name="type"></param>
        /// <param name="data"></param>
        private void GetUserInfo(int reqID, ResponseState state, PlatformType type, Hashtable data)
        {
            Debug.Log("获取用户信息回调");
            switch (state)
            {
                case ResponseState.Success:
                    
                    JsonData userData = JsonMapper.ToObject(JsonMapper.ToJson(data));
                    text.text += "\n userid : " + userData["userID"] + "\n username : " + userData["nickname"] + "\n icon : " + userData["icon"];
                    text.text += "\n获取信息成功！！！";
                    userName.text = userData["nickname"].ToString();
                    break;
                case ResponseState.Fail:
                    text.text += "\n获取信息失败！！！";
                    break;
            }
        }
        /// <summary>
        /// 登录回调
        /// </summary>
        /// <param name="reqID"></param>
        /// <param name="state"></param>
        /// <param name="type"></param>
        /// <param name="data"></param>
        private void AuthHandler(int reqID, ResponseState state, PlatformType type, Hashtable data)
        {
            Debug.Log("回调函数");
            if (state == ResponseState.Success)
            {
                JsonData userData = JsonMapper.ToObject(JsonMapper.ToJson(data));
                SaveUserInfo(JsonMapper.ToJson(data));
                string icon = userData["icon"].ToString();
                StartCoroutine(DownUserIcon(icon));
                text.text += "\n userid : " + userData["userID"] + "\n username : " + userData["nickname"] + "\n icon : " + userData["icon"];
                text.text += "\n授权成功！！！";
                userName.text = userData["nickname"].ToString();

            }
            else if (state == ResponseState.Fail)
            {
                text.text += "\n授权失败！！！";
            }
        }
        /// <summary>
        /// 将用户的头像下载
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        private IEnumerator DownUserIcon(string icon)
        {
            Debug.Log("开启协程进行资源下载");
            WWW www = new WWW(icon);
            yield return www;
            image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
            FileStream stream = File.Create(Application.persistentDataPath + iconName);
            Texture2D texture = new Texture2D(www.texture.width, www.texture.height);
            www.LoadImageIntoTexture(texture);
            byte[] bytes = texture.EncodeToJPG();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            stream.Dispose();
        }
        /// <summary>
        /// 将得到的用户信息保存
        /// </summary>
        /// <param name="jsonFile"></param>
        private void SaveUserInfo(string jsonFile)
        {
            if (File.Exists(Application.persistentDataPath + "/" + fileName))
                File.Delete(Application.persistentDataPath + "/" + fileName);
            File.WriteAllText(Application.persistentDataPath + "/"+fileName, jsonFile);
        }
    }
}