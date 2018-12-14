# TaskScheduling
quartz.net与CrystalQuartz的示例

任务调度在我们日常开发过程中非常常见，比如：每天晚上0点自动执行某某操作；每周三晚上2点执行某某操作；......当然，我们处理这类问题的方法也有很多，比如：sql的自动任务；windows上创建任务计划；写windows服务等等。如果系统比较复杂，相互调用比较频繁，任务非常多，几百上千条甚至上万条，那么本身对任务的管理就是比较昂贵的代价；如何提高任务的高可用？任务的测试是否便捷等等问题就会出现。上述的方案是否还能从容应对？
这时我们就迫切地需要一个作业调度系统来处理这些场景。
Quartz.NET是一个强大、开源、轻量的作业调度框架，是 OpenSymphony 的 Quartz API 的.NET移植，它有很多特征，如：数据库支持，集群，插件，支持cron-like表达式等等。官方网址：https://www.quartz-scheduler.net；GitHub地址：
https://github.com/quartznet/quartznet，各种用法可以参考示例程序。
但如果想方便的知道某个作业执行情况，需要暂停，启动等操作行为，这时候就需要个Job管理的界面。CrystalQuartz可以实现远程管理。
多数系统都会涉及到“后台服务”的开发，一般是为了调度一些自动执行的任务或从队列中消费一些消息，开发 windows service 有一点不爽的是：调试麻烦，当然你还需要知道 windows service 相关的一些开发知识（也不难），TopShelf框架，可以你让 console application 封装为 windows service，这样你就非常方便的开发和调试 windows service。TopShelf框架的官网为Url:http://topshelf-project.com
用TopShelf和quartz.net编写任务，CrystalQuartz管理任务。本文就是搭建一个简易的任务调度方案，启动任务调度、添加Job、移除Job、远程管理等。
<h3>联系我们</h3>

![Image text](https://github.com/anangyang/TaskScheduling/blob/master/Images/weiChartPic.png)

<p>博客网址：http://net-yuan.com</p></p>
<p>Email：2253972318@qq.com</p>
<p>QQ：2253972318</p>
