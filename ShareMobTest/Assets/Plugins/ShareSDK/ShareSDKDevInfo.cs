using UnityEngine;
using System.Collections;
using System;

namespace cn.sharesdk.unity3d 
{
	[Serializable]
	public class DevInfoSet
	{
		public SinaWeiboDevInfo sinaweibo;
		public QQ qq;
		public QZone qzone;
		public WeChat wechat;
		public WeChatMoments wechatMoments; 
#if UNITY_ANDROID
#elif UNITY_IPHONE
        public Copy copy;
		public YixinFavorites yixinFavorites;					//易信收藏，仅iOS端支持							[仅支持iOS端]
		public YixinSeries yixinSeries;							//iOS端易信系列, 可直接配置易信三个子平台			[仅支持iOS端]
		public WechatSeries wechatSeries;						//iOS端微信系列, 可直接配置微信三个子平台 		[仅支持iOS端]
		public QQSeries qqSeries;								//iOS端QQ系列,  可直接配置QQ系列两个子平台		[仅支持iOS端]
		public KakaoSeries kakaoSeries;							//iOS端Kakao系列, 可直接配置Kakao系列两个子平台	[仅支持iOS端]
		public EvernoteInternational evernoteInternational;		//iOS配置印象笔记国内版在Evernote中配置;国际版在EvernoteInternational中配置
		public Telegram telegram;									
		#endif

	}

	public class DevInfo 
	{	
		public bool Enable = true;
	}

	[Serializable]
	public class SinaWeiboDevInfo : DevInfo 
	{
		#if UNITY_ANDROID
		public const int type = (int) PlatformType.SinaWeibo;
		public string SortId = "4";
		public string AppKey = "568898243";
		public string AppSecret = "38a4f8204cc784f81f9f0daaf31e02e3";
		public string RedirectUrl = "http://www.sharesdk.cn";
		public bool ShareByAppClient = false;
		#elif UNITY_IPHONE
		public const int type = (int) PlatformType.SinaWeibo;
		public string app_key = "568898243";
		public string app_secret = "38a4f8204cc784f81f9f0daaf31e02e3";
		public string redirect_uri = "http://www.sharesdk.cn";
//		public string auth_type = "both";	//can pass "both","sso",or "web"  
		#endif
	}
    
	[Serializable]
	public class QQ : DevInfo 
	{
		#if UNITY_ANDROID
		public const int type = (int) PlatformType.QQ;
		public string SortId = "2";
		public string AppId = "100371282";
		public string AppKey = "aed9b0303e3ed1e27bae87c33761161d";
		public bool ShareByAppClient = true;
		#elif UNITY_IPHONE
		public const int type = (int) PlatformType.QQ;
		public string app_id = "100371282";
		public string app_key = "aed9b0303e3ed1e27bae87c33761161d";
//		public string auth_type = "both";  //can pass "both","sso",or "web" 
		#endif
	}

	[Serializable]
	public class QZone : DevInfo 
	{
		#if UNITY_ANDROID
		public string SortId = "1";
		public const int type = (int) PlatformType.QZone;
		public string AppId = "100371282";
		public string AppKey = "ae36f4ee3946e1cbb98d6965b0b2ff5c";
		public bool ShareByAppClient = true;
		#elif UNITY_IPHONE
		public const int type = (int) PlatformType.QZone;
		public string app_id = "100371282";
		public string app_key = "aed9b0303e3ed1e27bae87c33761161d";
//		public string auth_type = "both";  //can pass "both","sso",or "web" 
		#endif
	}
	

	
	[Serializable]
	public class WeChat : DevInfo 
	{	
		#if UNITY_ANDROID
		public string SortId = "5";
		public const int type = (int) PlatformType.WeChat;
		public string AppId = "wx4868b35061f87885";
		public string AppSecret = "64020361b8ec4c99936c0e3999a9f249";
		public string UserName = "gh_afb25ac019c9@app";
		public string Path = "/page/API/pages/share/share";
		public bool BypassApproval = true;
		public bool WithShareTicket = true;
		public string MiniprogramType = "0";
		#elif UNITY_IPHONE
		public const int type = (int) PlatformType.WeChat;
		public string app_id = "wx4868b35061f87885";
		public string app_secret = "64020361b8ec4c99936c0e3999a9f249";
		#endif
	}

	[Serializable]
	public class WeChatMoments : DevInfo 
	{
		#if UNITY_ANDROID
		public string SortId = "6";
		public const int type = (int) PlatformType.WeChatMoments;
		public string AppId = "wx4868b35061f87885";
		public string AppSecret = "64020361b8ec4c99936c0e3999a9f249";
		public bool BypassApproval = true;
		#elif UNITY_IPHONE
		public const int type = (int) PlatformType.WeChatMoments;
		public string app_id = "wx4868b35061f87885";
		public string app_secret = "64020361b8ec4c99936c0e3999a9f249";
		#endif
	}
	// 		安卓描述:   
	//		在中国大陆，印象笔记有两个服务器，一个是沙箱（sandbox），一个是生产服务器（china）。
	//		一般你注册应用，它会先让你使用sandbox，当你完成测试以后，可以到
	//		http://dev.yinxiang.com/support/上激活你的ConsumerKey，激活成功后，修改HostType
	//		为china就好了。至于如果您申请的是国际版的印象笔记（Evernote），则其生产服务器类型为
	//		“product”。
	//		如果目标设备上已经安装了印象笔记客户端，ShareSDK允许应用调用本地API来完成分享，但
	//		是需要将应用信息中的“ShareByAppClient”设置为true，此字段默认值为false。
	//      

	//      iOS描述:
	//		配置好consumerKey 和 secret, 如果为沙箱模式，请对参数isSandBox传入非0值，否则传入0.

	//在以下的配置里，安卓请选择Evernote配置。
	//iOS则需要区分，国内版为Evernote，国际版EvernoteInternational。
    

	[Serializable]
	public class EvernoteInternational : DevInfo
	{
		#if UNITY_ANDROID
		//ANDROID do not support this platform
		#elif UNITY_IPHONE
		public const int type = (int)PlatformType.EvernoteInternational;  
		public string consumer_key = "sharesdk-7807";
		public string consumer_secret = "d05bf86993836004";
		public int isSandBox = 0; //"0" mean NO with SandBox, !0 mean YES with SandBox
		#endif
	}
    
	[Serializable]
	public class YixinSeries : DevInfo 
	{
		#if UNITY_ANDROID
		//for android,please set the configuraion in class "Yixin" or class "YixinMoments" 
		//(Android do not support YixinFavorites)
			//对于安卓端，Yixin或YixinMoments中配置相关信息(安卓端不支持易信收藏YixinFavorites)
		#elif UNITY_IPHONE
		public const int type = (int) PlatformType.YixinPlatform;
		public string app_id = "yx0d9a9f9088ea44d78680f3274da1765f";
		public string app_secret = "1a5bd421ae089c3";
		public string redirect_uri = "https://open.yixin.im/resource/oauth2_callback.html";
//		public string auth_type = "both";   //can pass "both","sso",or "web" 
		#endif
	}

	[Serializable]
	public class YixinFavorites : DevInfo 
	{
		#if UNITY_ANDROID
		//for android,please set the configuraion in class "Yixin" or class "YixinMoments" 
		//(Android do not support YixinFavorites)
		//对于安卓端，Yixin或YixinMoments中配置相关信息(安卓端不支持易信收藏YixinFavorites)
		#elif UNITY_IPHONE
		public const int type = (int) PlatformType.YiXinFav;
		public string app_id = "yx0d9a9f9088ea44d78680f3274da1765f";
		public string app_secret = "1a5bd421ae089c3";
		public string redirect_uri = "https://open.yixin.im/resource/oauth2_callback.html";
//		public string auth_type = "both";   //can pass "both","sso",or "web" 
		#endif
	}
		
	[Serializable]
	public class WechatSeries : DevInfo 
	{	
		#if UNITY_ANDROID
		//for android,please set the configuraion in class "Wechat" ,class "WechatMoments" or class "WechatFavorite"
		//对于安卓端，请在类Wechat,WechatMoments或WechatFavorite中配置相关信息↑	
		#elif UNITY_IPHONE
		public const int type = (int) PlatformType.WechatPlatform;
		public string app_id = "wx4868b35061f87885";
		public string app_secret = "64020361b8ec4c99936c0e3999a9f249";
		#endif
	}

	[Serializable]
	public class QQSeries : DevInfo 
	{	
		#if UNITY_ANDROID
		//for android,please set the configuraion in class "QQ" and  class "QZone"
		//对于安卓端，请在类QQ或QZone中配置相关信息↑	
		#elif UNITY_IPHONE
		public const int type = (int) PlatformType.QQPlatform;
		public string app_id = "100371282";
		public string app_key = "aed9b0303e3ed1e27bae87c33761161d";
//		public string auth_type = "both";  //can pass "both","sso",or "web" 
		#endif
	}

	[Serializable]
	public class KakaoSeries : DevInfo 
	{
		#if UNITY_ANDROID
		//for android,please set the configuraion in class "KakaoTalk" and  class "KakaoStory"
		//对于安卓端，请在类KakaoTalk或KakaoStory中配置相关信息
		#elif UNITY_IPHONE
		public const int type = (int) PlatformType.KakaoPlatform;
		public string app_key = "48d3f524e4a636b08d81b3ceb50f1003";
		public string rest_api_key = "ac360fa50b5002637590d24108e6cb10";
		public string redirect_uri = "http://www.mob.com/oauth";
//		public string auth_type = "both";   //can pass "both","sso",or "web" 
		#endif
	}
    
	
	//[Serializable]		
	//public class Telegram : DevInfo		
	//{		
	//	#if UNITY_ANDROID		
	//	public string SortId = "47";		
	//	public const int type = (int) PlatformType.Telegram;		
	//	#elif UNITY_IPHONE		
	//	#endif		
	//}

    #if UNITY_ANDROID
    //[Serializable]
   // public class CMCC : DevInfo
   // {
   //     public string SortId = "55";
   //     public const int type = (int) PlatformType.CMCC;
   //     public string AppId = "300011860247";
   //     public string AppKey = "2D464D8BFCE73A44B4F9DF95A2FDBE1C";
   //}
    
    #elif UNITY_IPHONE
	[Serializable]		
	public class CMCC : DevInfo		
	{		
		public string app_id = "300011862498";
		public string app_key = "38D9CA1CC280C5F207E2C343745D4A4B";
		public int displayUI = 1; // 1 显示授权界面  0 不显示授权界面 
		public const int type = (int) PlatformType.CMCC;
	}

	[Serializable]
	public class Telegram : DevInfo		
	{	public string bot_token = "600852601:AAElp9J93JiYevLocDIEYPhEYulnMFuB_nQ";
		public string bot_domain = "http://127.0.0.1";
		public const int type = (int) PlatformType.Telegram;
	}

	[Serializable]
	public class Reddit : DevInfo		
	{
		public string app_key = "ObzXn50T7Cg0Xw";
		public string redirect_uri = "https://www.mob.com/reddit_callback";
		public const int type = (int) PlatformType.Reddit;
	}
    #endif

}
