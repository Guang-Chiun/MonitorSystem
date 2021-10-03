# AOI機台監控程式
- 開發環境 : C# Net Core 3.0
- [GitHub連結](https://github.com/Guang-Chiun/MonitorSystem)
- [程式使用資料庫(SQL Server)連結](https://drive.google.com/file/d/1xvTrdltPyeFT5Z-mevcanZZjB6h7T2AH/view)


## 主畫面
![](https://i.imgur.com/MPB9q5U.gif)


## AOI簡介
自動光學檢查（Automated Optical Inspection，簡稱AOI），為高速高精度光學影像檢測系統，運用機器視覺做為檢測標準技術，改良傳統上以人力使用光學儀器進行檢測的缺點。AOI於產線中可檢查料片之缺陷異常。


## 程式功能簡介
該系統可用於監控產線中AOI機台之異常狀況，監測類型主要為以下兩點:


#### 1. AOI機台本身異常 : 
若是機台發生問題(EX : CCD 異常、進片異常......)，導致機台無法繼續生產，機台端會透過Socket發送訊息告知系統，而系統畫面中對應的機台元件會視警報的嚴重性改變顏色，用以提示使用者。

#### 2. AOI機台生產資料問題
目前程式以檢測面板玻璃資料為例，若是機台生產出來的面板有問題(EX : 缺陷過多、有曝光機的缺陷......)，系統畫面中的機台UI也會視問題的嚴重性改變顏色，提示使用者。並可點選該EQ Unit查看該機台目前生產情形圖表。


## 功能詳細介紹
- [EQ UI元件變色功能](https://hackmd.io/@TaNq7dHGRLWgeV6SVPERyQ/HytbFwXNt)
- [監控圖表功能簡介](https://hackmd.io/@TaNq7dHGRLWgeV6SVPERyQ/r1-5IfVVF)
- [設定json內容動態建立UI](https://hackmd.io/@TaNq7dHGRLWgeV6SVPERyQ/rkIz4E4VK)

 
## 專案中練習到的一些功能
本次專案的目的，即是希望透過專案練習C#中一些進階的功能、SQL Server功能，以及使用design Pattern實作一些功能。

### C#進階功能
- [介面(Interface)](https://hackmd.io/@TaNq7dHGRLWgeV6SVPERyQ/By_wtSVNt) 
- [委派(Action、Func)](https://hackmd.io/@TaNq7dHGRLWgeV6SVPERyQ/Hkc7aIVVt)
- [反射(Reflection)](https://hackmd.io/@TaNq7dHGRLWgeV6SVPERyQ/BJgLdP4Vt)
- [自定義Attribute](https://hackmd.io/@TaNq7dHGRLWgeV6SVPERyQ/SJGW0o44K)
- [使用者控制項元件建立(UserControl)](https://hackmd.io/@TaNq7dHGRLWgeV6SVPERyQ/r1LzsCH4F)
- [非同步練習](https://hackmd.io/@TaNq7dHGRLWgeV6SVPERyQ/By6HCyI4F)[target=_blank]

### SQL Server
- [資料庫預存程序(Stored Procedure)](https://hackmd.io/@TaNq7dHGRLWgeV6SVPERyQ/r1BqygLNY)

### Design Pattern
- [工廠模式(Factory Pattern)](https://hackmd.io/@TaNq7dHGRLWgeV6SVPERyQ/Hyy_VgL4K)

### 開發專案遇到的一些問題分享
- [Net Core 3.0以上不支援MS Chart工具了!!!???](https://hackmd.io/@TaNq7dHGRLWgeV6SVPERyQ/BkpjEx8EF)



### 未來規劃
- 目前程式僅能監控AOI機台，產線中機台種類眾多，後續擴充往不只能監控AOI機台方向邁進。
- 目前程式為監控單一機台，後續方向希望能將機台串成產線，每個EQ UI元件可加入:等待的生產量、該機台實際的生產量，如此程式可用來觀察產能，或是哪台機檯導致塞片問題......。





### 參考資料
- [資料庫內圖表變動即時通知C#程式工具](https://github.com/christiandelbianco/monitor-table-change-with-sqltabledependency)
- [用C#實作聊天室功能](https://github.com/yinyoupoet/chatRoomTest)
- [市面廠商監控程式畫面UI參考1](http://www.kingroupsys.com/index.php?option=module&lang=cht&task=pageinfo&id=37&index=4)
- [市面廠商監控程式畫面UI參考2](https://www.youmelive.com/keji/346520.html)
- [市面廠商監控程式畫面UI參考3](https://www.wavejet.com.tw/%e5%b7%a5%e6%a5%ad4-0-%e6%99%ba%e8%83%bd%e5%b7%a5%e5%bb%a0-%e8%a8%ad%e5%82%99%e7%8b%80%e6%85%8b%e7%9b%a3%e6%8e%a7%e5%b0%88%e5%8d%80/)