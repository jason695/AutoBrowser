Welcome to the AutoBrowser wiki!
# 打卡記錄:
* 1.MAIL通知,寄件者為SQLDB@sinopac.com,透過10.11.6.34的DB發送
* 2.LOG留存,路徑為C:\AutoBrowser_log\AutoBrowser_log.txt
* 3.畫面備份,路徑為C:\AutoBrowser_log\screenYYYYMMDD.jpg

------------------------------
# 修改記錄:
## @20170201 (V2.4.3)
* 1.在啟動時,取回檔案至本機(注意效能問題)
* 2.下拉選單的讀檔順序為: 遠端1 > 遠端2 > 本機
  ** 遠端1:\\10.11.22.51\d$\113720\list_p.txt --> path1
  ** 遠端2:\\10.11.34.172\c$\list_p.txt --> path2
  ** 本機:C:\\AutoBrowser_log\\list_p.txt --> path3

## @20170201 (V2.4.4)
* 1.加快開啟速度,連線磁碟改非同步
* 2.下拉選單以本機檔案為先,後讀遠端

## @20170223 (V2.4.4.1)
* 1.寄信改為呼叫本機OUTLOOK

## @20170426 (V2.4.4.2)
* 1.加入github (https://github.com/jason695/AutoBrowser.git)
* 2.名單檔同步機制.jpg
帳密會儲存地點、取用的優先順序如下
	* * 1.使用遠端1連線 --> "10.11.34.172"
	* * 2.使用遠端2連線 --> "10.11.22.51"
	* * 3.使用本機連線

指令為:
net use \\@remoteHost @passWord /user:@userName

** V2.4.4.1會因為有已設定遠端磁碟已而無法使用
** V2.4.4.2已解決此問題

## @20170516 (V2.4.4.3)
* 1.移除下拉選單同步 FOR 昊澐

## @20170622 (V2.4.5.1)
* 1.使用MONGODB同步,安裝於
	* * 10.11.9.191 (win2008)
	* * 10.11.42.37 (win2008)

* 2.相關設定
	--環境變數path新增
	C:\Program Files\MongoDB\Server\3.4\bin

	--建目錄
	C:\>mkdir mongodb\dbwt

	--在Windows中以服務方式啟動MongoDB
	C:\>mongod --port 27017 --dbpath c:\mongodb\dbwt --logpath c:\mongodb\dbwt\dbwt.log --install --serviceName "MongoDBdbwt" --replSet jasons
	C:\>net start "MongoDBdbwt"
	MongoDB 服務正在啟動 ..
	MongoDB 服務已經啟動成功。

	--關閉服務
	C:\>net stop "MongoDBdbwt"

	--移除服務
	C:\>mongod --remove --serviceName "MongoDBdbwt"

	--連線
	mongo --host 10.11.9.191

	--設定replica set

## @20170712 (V2.4.5.2)
* 1.加入MONGODB名單更新通知
	* * mongo 10.11.9.191:27017
	* * mongo 10.11.42.37:27017
	* * mongo 10.11.34.59:27017
	* * Server=jasons/10.11.9.191:27017,10.11.42.37:27017,10.11.34.59:27017
* 2.DB資訊
	* * db: AutoBrowser
	* * collection: Products
	* * column: txt,ip,dtime
* 3.語法
	* * mongo 10.11.9.191:27017
	* * use AutoBrowser
	* * db.Products.find();
	* * db.Products_LOG.find();

* 4.匯出
	

## @20170918 (V2.4.5.3)
* 1.加入新mongo主機
* 2.新增TABLE:Products_log,新增&不刪除
* 3.維護名單前先同步DB

## @20171018 (V2.4.5.4)
* 1.維護名單修改密碼,若和預設ID相同,則同步更新LOCAL預設密碼

## @20171115 (V2.4.5.5)
* 1.排程&關機打卡功能,暫停OUTLOOK寄送

## @20171124 (V2.4.5.6)
* 1.改為登入永豐雲打卡

## @20171128 (V2.4.5.7)
* 1.新增LINE NOTIFY通知功能,相關說明請參考:支援LINE通知.docx

## @20171206 (V2.4.5.8)
* 1.排程打卡
* 2.登入異常/錯誤處理
* 3.新增推播通知
* 4.更新WEB SERVICE網址(http://35.189.190.46/LINENOTIFY/WS.asmx)
* 5.新增ICON

## @20180525 (V2.4.5.9)
* 1.強化打卡異常處理

## @20181213 (V2.4.6.0)
* 1.原CALL WebService改為Web API
* 2.URL還未確定

## @20191005 (V2.4.6.0)
* 1.新增10.11.34.172，移除10.11.42.37
    ** mongo 10.11.9.191:27017
    ** mongo 10.11.34.59:27017
    ** mongo 10.11.34.172:27017

## @20200724 (V2.4.6.0)
* 1.登入後遇到ALERT視窗,可以按確定跳過

## @20201112 (V2.4.6.0)
* 1.新增10.11.34.172，移除10.11.9.191
    ** mongo 10.11.34.59:27017
    ** mongo 10.11.34.172:27017
    ** mongo 10.11.36.192:27017
    ** 連線語法: mongo --host jasons/10.11.34.59:27017,10.11.34.172:27017,10.11.36.192:27017
