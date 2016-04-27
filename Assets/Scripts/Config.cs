using UnityEngine;
using System.Collections;

public class Question {
	public string question;
	public string[] answer;
	public int correct;
	public string right;
	public string wrong;

	public Question (string q, string[] a, int c, string r, string w)
	{
		question = q;
		answer = a;
		correct = c;
		right = r;
		wrong = w;
	}
};

public class Config {	
	
	public ArrayList questions = new ArrayList();
	public Config() {
		questions.Add(new Question("1. 亡灵鱼人被称作什么？ ", new string[]{"A. 鳄鱼人 ", "B. 行尸鱼人 ", "C. 狼獾人 ", "D. 巫妖鱼人 "}, 1, "Right", "Wrong"));
		questions.Add(new Question("2. 下面哪句话是龙语中“谢谢你”的意思？ ", new string[]{"A. ", "Borela mir ", "B. ", "Belan shi ", "C. Avral shi ", "D. Alena mir "}, 1, "Right", "Wrong"));
		questions.Add(new Question("3.  下面哪一位的墓志铭上刻有“愿血染的王冠永远被遗失和忘却”这句话？ ", new string[]{"A. 阿尔萨斯·米奈希尔王子 ", "B. 莱恩·乌瑞恩国王 ", "C. 乌瑟尔·光明使者 ", "D. 泰瑞纳斯·米奈希尔二世国王 "}, 3,"Right", "Wrong"));
		questions.Add(new Question("4. 在撕心狼变成狼人之前，他也曾有过家室。他的妻子叫什么名字？ ", new string[]{"A. 艾玛·哈林顿 ", "B. 卡莉莎·哈林顿 ", "C. 薇妮莎·怀特豪尔 ", "D. 卡特里娜·怀特豪尔 "}, 1, "Right", "Wrong"));
		questions.Add(new Question("5. 有一位王后，在大灾变和被遗忘者袭击她的国度时，她井井有条地安排了人民的疏散工作。这位王后叫什么名字？ ", new string[]{"A. 莉亚·格雷迈恩王后 ", "B. 玛利亚·格雷迈恩王后 ", "C. 莉莉娅·格雷迈恩王后 ", "D. 米亚·格雷迈恩王后 "}, 3,"Right", "Wrong"));
		questions.Add(new Question("6. 在上古之战后，守护巨龙们给了暗夜精灵什么东西？ ", new string[]{"A. 泰达希尔 ", "B. 永恒之井 ", "C. 诺达希尔 ", "D. 月亮井 "}, 2,"Right", "Wrong"));
		questions.Add(new Question("7. 不久之前，一个脆弱的赞达拉巨魔想要驯服一头恐角龙。虽然他登上了巨兽岛，却在任务过程中遇害。请问这个巨魔叫什么名字？ ", new string[]{"A. 玛尔卡 ", "B. 格里麦斯 ", "C. 塔拉克 ", "D. 拉维里 "}, 2,"Right", "Wrong"));
		questions.Add(new Question("8. 瓦里安·乌瑞恩国王的第一任妻子的正确名字是什么？ ", new string[]{"A. 蒂芬·温德米尔·乌瑞恩 ", "B. 泰丽丝·安妮·乌瑞恩 ", "C. 泰丽丝·安哈蕾德·乌瑞恩 ", "D. 蒂芬·艾莉安·乌瑞恩"}, 3,"Right", "Wrong"));
		questions.Add(new Question("9. 一位血色十字军保卫者在刺杀恐惧魔王贝塞利斯时遇害，她叫什么名字？ ", new string[]{"A. 菲尔拉蕾·迅箭 ", "B. 霍利亚·萨希尔德 ", "C. 亚娜·血矛 ", "D. 瓦雷亚 "}, 1,"Right", "Wrong"));
		questions.Add(new Question("10. 在担任家庭教师期间，斯塔文·密斯特曼托爱上了自己的学生，一个名叫蒂罗亚的年轻姑娘。请问她的弟弟叫什么名字？ ", new string[]{"A. 比利 ", "B. 威廉姆 ", "C. 基尔斯 ", "D. 托比亚斯 "}, 2,"Right", "Wrong"));
		questions.Add(new Question("11. 魅魔热衷于制造痛苦，她们在燃烧军团中专门负责进行可怕的拷问工作。请问魅魔属于哪个品种？ ", new string[]{"A. 埃雷杜因 ", "B. 萨亚德 ", "C. 破坏魔 ", "D. 艾瑞达 "}, 1,"Right", "Wrong"));
		questions.Add(new Question("12. 铁炉堡图书馆中展出了一具巨型山羊的骨架复制品。这头传奇山羊叫什么名字？ ", new string[]{"A. 污蹄 ", "B. 磨齿 ", "C. 血角 ", "D. 钢拳 "}, 1,"Right", "Wrong"));
		questions.Add(new Question("13. 艾泽拉斯出现的第一个死亡骑士是谁？ ", new string[]{"A. 塔隆·血魔 ", "B. 阿尔萨斯·米奈希尔 ", "C. 亚历山德罗斯·莫格莱尼 ", "D. 库尔迪拉·织亡者 "}, 0,"Right", "Wrong"));
		questions.Add(new Question("14. 头衔中包含“夜晚之友”的是哪位神灵？ ", new string[]{"A. 西瓦尔拉 ", "B. 吉布尔 ", "C. 缪萨拉 ", "D. 沙德拉 "}, 2,"Right", "Wrong"));
		questions.Add(new Question("15. 德鲁伊的最高称号是什么？ ", new string[]{"A. 萨满祭司 ", "B. 唤雷者 ", "C. 大德鲁伊 ", "D. 先知 "}, 2,"Right", "Wrong"));
		questions.Add(new Question("16. 由玛法里奥·怒风协助建立的，艾泽拉斯最主要的德鲁伊组织叫什么名字？ ", new string[]{"A. 深红之环 ", "B. 翡翠议会 ", "C. 大地之环 ", "D. 塞纳里奥议会 "}, 3,"Right", "Wrong"));
		questions.Add(new Question("17. 在牛头人的语言中，lar'korwi是什么意思？ ", new string[]{"A. 锋利的爪子 ", "B. 剃刀般的牙齿 ", "C. 锋利的牙齿 ", "D. 剃刀般的爪子 "}, 0,"Right", "Wrong"));
		questions.Add(new Question("18. 侏儒如今的领袖是谁？ ", new string[]{"A. 格尔宾·梅卡托克 ", "B. 米尔豪斯·法力风暴 ", "C. 希科·瑟玛普拉格 ", "D. 菲兹兰克·铁阀 "}, 0,"Right", "Wrong"));
		questions.Add(new Question("19. 白狼曾是哪个兽人氏族最喜爱的坐骑？ ", new string[]{"A. 白爪氏族 ", "B. 战歌氏族 ", "C. 霜狼氏族 ", "D. 冰牙氏族 "}, 2,"Right", "Wrong"));
		questions.Add(new Question("20. 虚灵的家乡叫什么名字？ ", new string[]{"A. 卡雷什 ", "B. 夏罗迪 ", "C. 库拉尔 ", "D. 克索诺斯 "}, 0,"Right", "Wrong"));
		questions.Add(new Question("21. 嫌弃银月城过于明亮和干净的部落大使叫什么名字？ ", new string[]{"A. 科内塔 ", "B. 克莉丝汀·德尼 ", "C. 塔泰 ", "D. 德拉·符文图腾 "}, 2,"Right", "Wrong"));
		questions.Add(new Question("22. 由地精制造，原计划搭载萨尔和阿格娜前往大漩涡，却被联盟意外摧毁的部落舰船叫什么名字？ ", new string[]{"A. 德拉卡的狂怒 ", "B. 地狱咆哮的狂怒 ", "C. 阿格娜的狂怒 ", "D. 杜隆坦的狂怒 "}, 0, "Right", "Wrong"));
		questions.Add(new Question("23. 大领主库德兰·蛮锤最近痛失爱将，他英勇的狮鹫在一次大火中不幸丧生。这头狮鹫叫什么名字？ ", new string[]{"A. 迅捷之翼 ", "B. 沙普比克 ", "C. 斯卡雷 ", "D. 风暴比克 "}, 2,"Right", "Wrong"));
		questions.Add(new Question("24. 在第三次大战过去数年之后，黑暗之门重新开启时，艾泽拉斯首次出现了棕色皮肤的兽人。这些兽人被称为什么？ ", new string[]{"A. 玛格汉兽人 ", "B. 魔血兽人 ", "C. 莫克纳萨兽人 ", "D. 邪兽人 "}, 0,"Right", "Wrong"));
		questions.Add(new Question("25. 阿尔萨斯用来训练死亡骑士，后来又由于大部分死亡骑士背叛巫妖王而被占领的浮空堡垒叫什么名字？ ", new string[]{"A. 阿彻鲁斯 ", "B. 纳克萨纳尔 ", "C. 科尔拉玛斯 ", "D. 纳克萨玛斯 "}, 0,"Right", "Wrong"));
		questions.Add(new Question("26. 这个世界上的第一个萨特是谁？ ", new string[]{"A. 佩罗萨恩 ", "B. 贾拉克萨斯 ", "C. 萨维斯 ", "D. 维利塔恩 "}, 2,"Right", "Wrong"));
		questions.Add(new Question("27. 在突袭冰冠堡垒时，部落军队无耻地偷袭正与天灾军团激战的联盟士兵，并试图占领的那座城门叫什么名字？ ", new string[]{"A. 科雷萨 ", "B. 奥尔杜萨 ", "C. 安加萨 ", "D. 莫德雷萨 "}, 3,"Right", "Wrong"));
		questions.Add(new Question("28. 被洛肯捕获并变成锋鳞的强大始祖龙叫什么名字？ ", new string[]{"A. 迦拉克隆 ", "B. 维拉努斯 ", "C. 塞普泰克 ", "D. 维利塔恩 "}, 1,"Right", "Wrong"));
		questions.Add(new Question("29. 在最初的部落建立之前，有一种高传染性的病毒在兽人间快速传播。兽人们称这种病为什么？ ", new string[]{"A. 猩红热 ", "B. 血色天灾 ", "C. 血红热 ", "D. 红色天灾 "}, 3,"Right", "Wrong"));
		questions.Add(new Question("30. 在第三次大战期间，是什么证据使阿尔萨斯王子屠杀了斯坦索姆的居民？ ", new string[]{"A. 被污染的泥土 ", "B. 被污染的野生动物 ", "C. 被污染的水 ", "D. 被污染的粮食 "}, 3,"Right", "Wrong"));
		questions.Add(new Question("31. 赞加沼泽有一个地方，那里为纳迦所控制，他们还试图在那里抽取一种珍贵而稀有的资源：外域之水。这个地方叫什么名字？ ", new string[]{"A. 盘蛇洞穴 ", "B. 蛇形盆地 ", "C. 盘牙水库 ", "D. 旋牙水潭 "}, 2,"Right", "Wrong"));
		questions.Add(new Question("32. 提里奥·弗丁的灰色公马叫什么？ ", new string[]{"A. 阿什纳尔 ", "B. 米瑟维尔 ", "C. 菲奥尼尔 ", "D. 米拉多尔 "}, 3, "Right", "Wrong"));
		questions.Add(new Question("33. 在黑曜石圣殿守护暮光龙蛋的三头暮光幼龙分别叫什么？ ", new string[]{"A. 塔尼布隆、维斯匹隆和沙德隆 ", "B. 塔尼布隆、维斯匹隆和海里昂 ", "C. 瑟纳利昂、沙德隆和艾比希昂 ", "D. 瑟纳利昂、海里昂和艾比希昂 "}, 0,"Right", "Wrong"));
		questions.Add(new Question("34. 隶属千万神殿精锐的泰坦知识守护者叫什么名字？ ", new string[]{"A. 阿曼苏尔 ", "B. 诺甘农 ", "C. 艾欧纳尔 ", "D. 卡兹格罗斯 "}, 1,"Right", "Wrong"));
		questions.Add(new Question("35. 在被阿尔萨斯复活为亡灵，并加入天灾军团之前，辛达苟萨曾属于哪个巨龙军团？ ", new string[]{"A. 红龙军团 ", "B. 蓝龙军团 ", "C. 绿龙军团 ", "D. 青铜龙军团 "}, 1,"Right", "Wrong"));
		questions.Add(new Question("36. 根据德莱尼人的玩笑，“埃索达”在纳鲁语中是什么意思？ ", new string[]{"A. 残次的雷象粪便 ", "B. 水晶死亡陷阱 ", "C. 毫无价值的雷象粪便 ", "D. 放射性生化灾难 "}, 0,"Right", "Wrong"));
		questions.Add(new Question("37. 有一位德莱尼人，他曾是一个健康的圣骑士，但在与燃烧军团作战时，他身染恶疾，变成了破碎者。后来，他成了一个强大的萨满。这个德莱尼人是谁？ ", new string[]{"A. 维纶 ", "B. 阿卡玛 ", "C. 玛尔拉得 ", "D. 努波顿 "}, 3,"Right", "Wrong"));
	}
}
