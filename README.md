Welcome to the AutoBrowser wiki!
# ���d�O��:
* 1.MAIL�q��,�H��̬�SQLDB@sinopac.com,�z�L10.11.6.34��DB�o�e
* 2.LOG�d�s,���|��C:\AutoBrowser_log\AutoBrowser_log.txt
* 3.�e���ƥ�,���|��C:\AutoBrowser_log\screenYYYYMMDD.jpg

------------------------------
# �ק�O��:
## @20170201 (V2.4.3)
* 1.�b�Ұʮ�,���^�ɮצܥ���(�`�N�į���D)
* 2.�U�Կ�檺Ū�ɶ��Ǭ�: ����1 > ����2 > ����
  * * ����1:\\10.11.22.51\d$\113720\list_p.txt --> path1
  * * ����2:\\10.11.34.172\c$\list_p.txt --> path2
  * * ����:C:\\AutoBrowser_log\\list_p.txt --> path3

## @20170201 (V2.4.4)
* 1.�[�ֶ}�ҳt��,�s�u�ϺЧ�D�P�B
* 2.�U�Կ��H�����ɮ׬���,��Ū����

## @20170223 (V2.4.4.1)
* 1.�H�H�אּ�I�s����OUTLOOK

## @20170426 (V2.4.4.2)
* 1.�[�Jgithub (https://github.com/jason695/AutoBrowser.git)
* 2.�W���ɦP�B����.jpg
�b�K�|�x�s�a�I�B���Ϊ��u�����Ǧp�U
	* * 1.�ϥλ���1�s�u --> "10.11.34.172"
	* * 2.�ϥλ���2�s�u --> "10.11.22.51"
	* * 3.�ϥΥ����s�u

���O��:
net use \\@remoteHost @passWord /user:@userName

* * V2.4.4.1�|�]�����w�]�w���ݺϺФw�ӵL�k�ϥ�
* * V2.4.4.2�w�ѨM�����D

## @20170516 (V2.4.4.3)
* 1.�����U�Կ��P�B FOR ���X

## @20170622 (V2.4.5.1)
* 1.�ϥ�MONGODB�P�B,�w�˩�
	* * 10.11.9.191 (win2008)
	* * 10.11.42.37 (win2008)

* 2.�����]�w
	--�����ܼ�path�s�W
	C:\Program Files\MongoDB\Server\3.4\bin

	--�إؿ�
	C:\>mkdir mongodb\dbwt

	--�bWindows���H�A�Ȥ覡�Ұ�MongoDB
	C:\>mongod --port 27017 --dbpath c:\mongodb\dbwt --logpath c:\mongodb\dbwt\dbwt.log --install --serviceName "MongoDBdbwt" --replSet jasons
	C:\>net start "MongoDBdbwt"
	MongoDB �A�ȥ��b�Ұ� ..
	MongoDB �A�Ȥw�g�Ұʦ��\�C

	--�����A��
	C:\>net stop "MongoDBdbwt"

	--�����A��
	C:\>mongod --remove --serviceName "MongoDBdbwt"

	--�s�u
	mongo --host 10.11.9.191

	--�]�wreplica set

## @20170712 (V2.4.5.2)
* 1.�[�JMONGODB�W���s�q��
	* * mongo 10.11.9.191:27017
	* * mongo 10.11.42.37:27017
	* * mongo 10.11.34.59:27017
	* * Server=jasons/10.11.9.191:27017,10.11.42.37:27017,10.11.34.59:27017
* 2.DB��T
	* * db: AutoBrowser
	* * collection: Products
	* * column: txt,ip,dtime
* 3.�y�k
	* * mongo 10.11.9.191:27017
	* * use AutoBrowser
	* * db.Products.find();
	* * db.Products_LOG.find();

* 4.�ץX
	mongoexport --host 10.11.34.59 -d AutoBrowser -c Products_LOG -o output.json

## @20170918 (V2.4.5.3)
* 1.�[�J�smongo�D��
* 2.�s�WTABLE:Products_log,�s�W&���R��
* 3.���@�W��e���P�BDB

## @20171018 (V2.4.5.4)
* 1.���@�W��ק�K�X,�Y�M�w�]ID�ۦP,�h�P�B��sLOCAL�w�]�K�X

## @20171115 (V2.4.5.5)
* 1.�Ƶ{&�������d�\��,�Ȱ�OUTLOOK�H�e

## @20171124 (V2.4.5.6)
* 1.�אּ�n�J���׶����d

## @20171128 (V2.4.5.7)
* 1.�s�WLINE NOTIFY�q���\��,���������аѦ�:�䴩LINE�q��.docx

## @20171206 (V2.4.5.8)
* 1.�Ƶ{���d
* 2.�n�J���`/���~�B�z
* 3.�s�W�����q��
* 4.��sWEB SERVICE���}(http://35.189.190.46/LINENOTIFY/WS.asmx)
* 5.�s�WICON

## @20180525 (V2.4.5.9)
* 1.�j�ƥ��d���`�B�z

## @20181213 (V2.4.6.0)
* 1.��CALL WebService�אּWeb API
* 2.URL�٥��T�w

## @20191005 (V2.4.6.0)
* 1.�s�W10.11.34.172�A����10.11.42.37
    * * mongo 10.11.9.191:27017
    * * mongo 10.11.34.59:27017
    * * mongo 10.11.34.172:27017

## @20200724 (V2.4.6.0)
* 1.�n�J��J��ALERT����,�i�H���T�w���L

## @20201112 (V2.4.6.0)
* 1.�s�W10.11.34.172�A����10.11.9.191
    * mongo 10.11.34.59:27017
    * mongo 10.11.34.172:27017
    * mongo 10.11.36.192:27017
    * �s�u�y�k: mongo --host jasons/10.11.34.59:27017,10.11.34.172:27017,10.11.36.192:27017

## @20220817 (V2.4.6.1)
* 1.LINE�J��TLS1.0����,��g�k��TLS 1.2