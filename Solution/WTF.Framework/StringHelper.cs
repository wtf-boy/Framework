namespace WTF.Framework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringHelper
    {
        private const int firstChCode = -20319;
        private const int lastChCode = -2050;
        private const int lastOfOneLevelChCode = -10247;
        private static string[] otherChinese = new string[] { 
            "亍", "丌", "兀", "丐", "廿", "卅", "丕", "亘", "丞", "鬲", "孬", "噩", "丨", "禺", "丿", "匕", 
            "乇", "夭", "爻", "卮", "氐", "囟", "胤", "馗", "毓", "睾", "鼗", "丶", "亟", "鼐", "乜", "乩", 
            "亓", "芈", "孛", "啬", "嘏", "仄", "厍", "厝", "厣", "厥", "厮", "靥", "赝", "匚", "叵", "匦", 
            "匮", "匾", "赜", "卦", "卣", "刂", "刈", "刎", "刭", "刳", "刿", "剀", "剌", "剞", "剡", "剜", 
            "蒯", "剽", "劂", "劁", "劐", "劓", "冂", "罔", "亻", "仃", "仉", "仂", "仨", "仡", "仫", "仞", 
            "伛", "仳", "伢", "佤", "仵", "伥", "伧", "伉", "伫", "佞", "佧", "攸", "佚", "佝", "佟", "佗", 
            "伲", "伽", "佶", "佴", "侑", "侉", "侃", "侏", "佾", "佻", "侪", "佼", "侬", "侔", "俦", "俨", 
            "俪", "俅", "俚", "俣", "俜", "俑", "俟", "俸", "倩", "偌", "俳", "倬", "倏", "倮", "倭", "俾", 
            "倜", "倌", "倥", "倨", "偾", "偃", "偕", "偈", "偎", "偬", "偻", "傥", "傧", "傩", "傺", "僖", 
            "儆", "僭", "僬", "僦", "僮", "儇", "儋", "仝", "氽", "佘", "佥", "俎", "龠", "汆", "籴", "兮", 
            "巽", "黉", "馘", "冁", "夔", "勹", "匍", "訇", "匐", "凫", "夙", "兕", "亠", "兖", "亳", "衮", 
            "袤", "亵", "脔", "裒", "禀", "嬴", "蠃", "羸", "冫", "冱", "冽", "冼", "凇", "冖", "冢", "冥", 
            "讠", "讦", "讧", "讪", "讴", "讵", "讷", "诂", "诃", "诋", "诏", "诎", "诒", "诓", "诔", "诖", 
            "诘", "诙", "诜", "诟", "诠", "诤", "诨", "诩", "诮", "诰", "诳", "诶", "诹", "诼", "诿", "谀", 
            "谂", "谄", "谇", "谌", "谏", "谑", "谒", "谔", "谕", "谖", "谙", "谛", "谘", "谝", "谟", "谠", 
            "谡", "谥", "谧", "谪", "谫", "谮", "谯", "谲", "谳", "谵", "谶", "卩", "卺", "阝", "阢", "阡", 
            "阱", "阪", "阽", "阼", "陂", "陉", "陔", "陟", "陧", "陬", "陲", "陴", "隈", "隍", "隗", "隰", 
            "邗", "邛", "邝", "邙", "邬", "邡", "邴", "邳", "邶", "邺", "邸", "邰", "郏", "郅", "邾", "郐", 
            "郄", "郇", "郓", "郦", "郢", "郜", "郗", "郛", "郫", "郯", "郾", "鄄", "鄢", "鄞", "鄣", "鄱", 
            "鄯", "鄹", "酃", "酆", "刍", "奂", "劢", "劬", "劭", "劾", "哿", "勐", "勖", "勰", "叟", "燮", 
            "矍", "廴", "凵", "凼", "鬯", "厶", "弁", "畚", "巯", "坌", "垩", "垡", "塾", "墼", "壅", "壑", 
            "圩", "圬", "圪", "圳", "圹", "圮", "圯", "坜", "圻", "坂", "坩", "垅", "坫", "垆", "坼", "坻", 
            "坨", "坭", "坶", "坳", "垭", "垤", "垌", "垲", "埏", "垧", "垴", "垓", "垠", "埕", "埘", "埚", 
            "埙", "埒", "垸", "埴", "埯", "埸", "埤", "埝", "堋", "堍", "埽", "埭", "堀", "堞", "堙", "塄", 
            "堠", "塥", "塬", "墁", "墉", "墚", "墀", "馨", "鼙", "懿", "艹", "艽", "艿", "芏", "芊", "芨", 
            "芄", "芎", "芑", "芗", "芙", "芫", "芸", "芾", "芰", "苈", "苊", "苣", "芘", "芷", "芮", "苋", 
            "苌", "苁", "芩", "芴", "芡", "芪", "芟", "苄", "苎", "芤", "苡", "茉", "苷", "苤", "茏", "茇", 
            "苜", "苴", "苒", "苘", "茌", "苻", "苓", "茑", "茚", "茆", "茔", "茕", "苠", "苕", "茜", "荑", 
            "荛", "荜", "茈", "莒", "茼", "茴", "茱", "莛", "荞", "茯", "荏", "荇", "荃", "荟", "荀", "茗", 
            "荠", "茭", "茺", "茳", "荦", "荥", "荨", "茛", "荩", "荬", "荪", "荭", "荮", "莰", "荸", "莳", 
            "莴", "莠", "莪", "莓", "莜", "莅", "荼", "莶", "莩", "荽", "莸", "荻", "莘", "莞", "莨", "莺", 
            "莼", "菁", "萁", "菥", "菘", "堇", "萘", "萋", "菝", "菽", "菖", "萜", "萸", "萑", "萆", "菔", 
            "菟", "萏", "萃", "菸", "菹", "菪", "菅", "菀", "萦", "菰", "菡", "葜", "葑", "葚", "葙", "葳", 
            "蒇", "蒈", "葺", "蒉", "葸", "萼", "葆", "葩", "葶", "蒌", "蒎", "萱", "葭", "蓁", "蓍", "蓐", 
            "蓦", "蒽", "蓓", "蓊", "蒿", "蒺", "蓠", "蒡", "蒹", "蒴", "蒗", "蓥", "蓣", "蔌", "甍", "蔸", 
            "蓰", "蔹", "蔟", "蔺", "蕖", "蔻", "蓿", "蓼", "蕙", "蕈", "蕨", "蕤", "蕞", "蕺", "瞢", "蕃", 
            "蕲", "蕻", "薤", "薨", "薇", "薏", "蕹", "薮", "薜", "薅", "薹", "薷", "薰", "藓", "藁", "藜", 
            "藿", "蘧", "蘅", "蘩", "蘖", "蘼", "廾", "弈", "夼", "奁", "耷", "奕", "奚", "奘", "匏", "尢", 
            "尥", "尬", "尴", "扌", "扪", "抟", "抻", "拊", "拚", "拗", "拮", "挢", "拶", "挹", "捋", "捃", 
            "掭", "揶", "捱", "捺", "掎", "掴", "捭", "掬", "掊", "捩", "掮", "掼", "揲", "揸", "揠", "揿", 
            "揄", "揞", "揎", "摒", "揆", "掾", "摅", "摁", "搋", "搛", "搠", "搌", "搦", "搡", "摞", "撄", 
            "摭", "撖", "摺", "撷", "撸", "撙", "撺", "擀", "擐", "擗", "擤", "擢", "攉", "攥", "攮", "弋", 
            "忒", "甙", "弑", "卟", "叱", "叽", "叩", "叨", "叻", "吒", "吖", "吆", "呋", "呒", "呓", "呔", 
            "呖", "呃", "吡", "呗", "呙", "吣", "吲", "咂", "咔", "呷", "呱", "呤", "咚", "咛", "咄", "呶", 
            "呦", "咝", "哐", "咭", "哂", "咴", "哒", "咧", "咦", "哓", "哔", "呲", "咣", "哕", "咻", "咿", 
            "哌", "哙", "哚", "哜", "咩", "咪", "咤", "哝", "哏", "哞", "唛", "哧", "唠", "哽", "唔", "哳", 
            "唢", "唣", "唏", "唑", "唧", "唪", "啧", "喏", "喵", "啉", "啭", "啁", "啕", "唿", "啐", "唼", 
            "唷", "啖", "啵", "啶", "啷", "唳", "唰", "啜", "喋", "嗒", "喃", "喱", "喹", "喈", "喁", "喟", 
            "啾", "嗖", "喑", "啻", "嗟", "喽", "喾", "喔", "喙", "嗪", "嗷", "嗉", "嘟", "嗑", "嗫", "嗬", 
            "嗔", "嗦", "嗝", "嗄", "嗯", "嗥", "嗲", "嗳", "嗌", "嗍", "嗨", "嗵", "嗤", "辔", "嘞", "嘈", 
            "嘌", "嘁", "嘤", "嘣", "嗾", "嘀", "嘧", "嘭", "噘", "嘹", "噗", "嘬", "噍", "噢", "噙", "噜", 
            "噌", "噔", "嚆", "噤", "噱", "噫", "噻", "噼", "嚅", "嚓", "嚯", "囔", "囗", "囝", "囡", "囵", 
            "囫", "囹", "囿", "圄", "圊", "圉", "圜", "帏", "帙", "帔", "帑", "帱", "帻", "帼", "帷", "幄", 
            "幔", "幛", "幞", "幡", "岌", "屺", "岍", "岐", "岖", "岈", "岘", "岙", "岑", "岚", "岜", "岵", 
            "岢", "岽", "岬", "岫", "岱", "岣", "峁", "岷", "峄", "峒", "峤", "峋", "峥", "崂", "崃", "崧", 
            "崦", "崮", "崤", "崞", "崆", "崛", "嵘", "崾", "崴", "崽", "嵬", "嵛", "嵯", "嵝", "嵫", "嵋", 
            "嵊", "嵩", "嵴", "嶂", "嶙", "嶝", "豳", "嶷", "巅", "彳", "彷", "徂", "徇", "徉", "後", "徕", 
            "徙", "徜", "徨", "徭", "徵", "徼", "衢", "彡", "犭", "犰", "犴", "犷", "犸", "狃", "狁", "狎", 
            "狍", "狒", "狨", "狯", "狩", "狲", "狴", "狷", "猁", "狳", "猃", "狺", "狻", "猗", "猓", "猡", 
            "猊", "猞", "猝", "猕", "猢", "猹", "猥", "猬", "猸", "猱", "獐", "獍", "獗", "獠", "獬", "獯", 
            "獾", "舛", "夥", "飧", "夤", "夂", "饣", "饧", "饨", "饩", "饪", "饫", "饬", "饴", "饷", "饽", 
            "馀", "馄", "馇", "馊", "馍", "馐", "馑", "馓", "馔", "馕", "庀", "庑", "庋", "庖", "庥", "庠", 
            "庹", "庵", "庾", "庳", "赓", "廒", "廑", "廛", "廨", "廪", "膺", "忄", "忉", "忖", "忏", "怃", 
            "忮", "怄", "忡", "忤", "忾", "怅", "怆", "忪", "忭", "忸", "怙", "怵", "怦", "怛", "怏", "怍", 
            "怩", "怫", "怊", "怿", "怡", "恸", "恹", "恻", "恺", "恂", "恪", "恽", "悖", "悚", "悭", "悝", 
            "悃", "悒", "悌", "悛", "惬", "悻", "悱", "惝", "惘", "惆", "惚", "悴", "愠", "愦", "愕", "愣", 
            "惴", "愀", "愎", "愫", "慊", "慵", "憬", "憔", "憧", "憷", "懔", "懵", "忝", "隳", "闩", "闫", 
            "闱", "闳", "闵", "闶", "闼", "闾", "阃", "阄", "阆", "阈", "阊", "阋", "阌", "阍", "阏", "阒", 
            "阕", "阖", "阗", "阙", "阚", "丬", "爿", "戕", "氵", "汔", "汜", "汊", "沣", "沅", "沐", "沔", 
            "沌", "汨", "汩", "汴", "汶", "沆", "沩", "泐", "泔", "沭", "泷", "泸", "泱", "泗", "沲", "泠", 
            "泖", "泺", "泫", "泮", "沱", "泓", "泯", "泾", "洹", "洧", "洌", "浃", "浈", "洇", "洄", "洙", 
            "洎", "洫", "浍", "洮", "洵", "洚", "浏", "浒", "浔", "洳", "涑", "浯", "涞", "涠", "浞", "涓", 
            "涔", "浜", "浠", "浼", "浣", "渚", "淇", "淅", "淞", "渎", "涿", "淠", "渑", "淦", "淝", "淙", 
            "渖", "涫", "渌", "涮", "渫", "湮", "湎", "湫", "溲", "湟", "溆", "湓", "湔", "渲", "渥", "湄", 
            "滟", "溱", "溘", "滠", "漭", "滢", "溥", "溧", "溽", "溻", "溷", "滗", "溴", "滏", "溏", "滂", 
            "溟", "潢", "潆", "潇", "漤", "漕", "滹", "漯", "漶", "潋", "潴", "漪", "漉", "漩", "澉", "澍", 
            "澌", "潸", "潲", "潼", "潺", "濑", "濉", "澧", "澹", "澶", "濂", "濡", "濮", "濞", "濠", "濯", 
            "瀚", "瀣", "瀛", "瀹", "瀵", "灏", "灞", "宀", "宄", "宕", "宓", "宥", "宸", "甯", "骞", "搴", 
            "寤", "寮", "褰", "寰", "蹇", "謇", "辶", "迓", "迕", "迥", "迮", "迤", "迩", "迦", "迳", "迨", 
            "逅", "逄", "逋", "逦", "逑", "逍", "逖", "逡", "逵", "逶", "逭", "逯", "遄", "遑", "遒", "遐", 
            "遨", "遘", "遢", "遛", "暹", "遴", "遽", "邂", "邈", "邃", "邋", "彐", "彗", "彖", "彘", "尻", 
            "咫", "屐", "屙", "孱", "屣", "屦", "羼", "弪", "弩", "弭", "艴", "弼", "鬻", "屮", "妁", "妃", 
            "妍", "妩", "妪", "妣", "妗", "姊", "妫", "妞", "妤", "姒", "妲", "妯", "姗", "妾", "娅", "娆", 
            "姝", "娈", "姣", "姘", "姹", "娌", "娉", "娲", "娴", "娑", "娣", "娓", "婀", "婧", "婊", "婕", 
            "娼", "婢", "婵", "胬", "媪", "媛", "婷", "婺", "媾", "嫫", "媲", "嫒", "嫔", "媸", "嫠", "嫣", 
            "嫱", "嫖", "嫦", "嫘", "嫜", "嬉", "嬗", "嬖", "嬲", "嬷", "孀", "尕", "尜", "孚", "孥", "孳", 
            "孑", "孓", "孢", "驵", "驷", "驸", "驺", "驿", "驽", "骀", "骁", "骅", "骈", "骊", "骐", "骒", 
            "骓", "骖", "骘", "骛", "骜", "骝", "骟", "骠", "骢", "骣", "骥", "骧", "纟", "纡", "纣", "纥", 
            "纨", "纩", "纭", "纰", "纾", "绀", "绁", "绂", "绉", "绋", "绌", "绐", "绔", "绗", "绛", "绠", 
            "绡", "绨", "绫", "绮", "绯", "绱", "绲", "缍", "绶", "绺", "绻", "绾", "缁", "缂", "缃", "缇", 
            "缈", "缋", "缌", "缏", "缑", "缒", "缗", "缙", "缜", "缛", "缟", "缡", "缢", "缣", "缤", "缥", 
            "缦", "缧", "缪", "缫", "缬", "缭", "缯", "缰", "缱", "缲", "缳", "缵", "幺", "畿", "巛", "甾", 
            "邕", "玎", "玑", "玮", "玢", "玟", "珏", "珂", "珑", "玷", "玳", "珀", "珉", "珈", "珥", "珙", 
            "顼", "琊", "珩", "珧", "珞", "玺", "珲", "琏", "琪", "瑛", "琦", "琥", "琨", "琰", "琮", "琬", 
            "琛", "琚", "瑁", "瑜", "瑗", "瑕", "瑙", "瑷", "瑭", "瑾", "璜", "璎", "璀", "璁", "璇", "璋", 
            "璞", "璨", "璩", "璐", "璧", "瓒", "璺", "韪", "韫", "韬", "杌", "杓", "杞", "杈", "杩", "枥", 
            "枇", "杪", "杳", "枘", "枧", "杵", "枨", "枞", "枭", "枋", "杷", "杼", "柰", "栉", "柘", "栊", 
            "柩", "枰", "栌", "柙", "枵", "柚", "枳", "柝", "栀", "柃", "枸", "柢", "栎", "柁", "柽", "栲", 
            "栳", "桠", "桡", "桎", "桢", "桄", "桤", "梃", "栝", "桕", "桦", "桁", "桧", "桀", "栾", "桊", 
            "桉", "栩", "梵", "梏", "桴", "桷", "梓", "桫", "棂", "楮", "棼", "椟", "椠", "棹", "椤", "棰", 
            "椋", "椁", "楗", "棣", "椐", "楱", "椹", "楠", "楂", "楝", "榄", "楫", "榀", "榘", "楸", "椴", 
            "槌", "榇", "榈", "槎", "榉", "楦", "楣", "楹", "榛", "榧", "榻", "榫", "榭", "槔", "榱", "槁", 
            "槊", "槟", "榕", "槠", "榍", "槿", "樯", "槭", "樗", "樘", "橥", "槲", "橄", "樾", "檠", "橐", 
            "橛", "樵", "檎", "橹", "樽", "樨", "橘", "橼", "檑", "檐", "檩", "檗", "檫", "猷", "獒", "殁", 
            "殂", "殇", "殄", "殒", "殓", "殍", "殚", "殛", "殡", "殪", "轫", "轭", "轱", "轲", "轳", "轵", 
            "轶", "轸", "轷", "轹", "轺", "轼", "轾", "辁", "辂", "辄", "辇", "辋", "辍", "辎", "辏", "辘", 
            "辚", "軎", "戋", "戗", "戛", "戟", "戢", "戡", "戥", "戤", "戬", "臧", "瓯", "瓴", "瓿", "甏", 
            "甑", "甓", "攴", "旮", "旯", "旰", "昊", "昙", "杲", "昃", "昕", "昀", "炅", "曷", "昝", "昴", 
            "昱", "昶", "昵", "耆", "晟", "晔", "晁", "晏", "晖", "晡", "晗", "晷", "暄", "暌", "暧", "暝", 
            "暾", "曛", "曜", "曦", "曩", "贲", "贳", "贶", "贻", "贽", "赀", "赅", "赆", "赈", "赉", "赇", 
            "赍", "赕", "赙", "觇", "觊", "觋", "觌", "觎", "觏", "觐", "觑", "牮", "犟", "牝", "牦", "牯", 
            "牾", "牿", "犄", "犋", "犍", "犏", "犒", "挈", "挲", "掰", "搿", "擘", "耄", "毪", "毳", "毽", 
            "毵", "毹", "氅", "氇", "氆", "氍", "氕", "氘", "氙", "氚", "氡", "氩", "氤", "氪", "氲", "攵", 
            "敕", "敫", "牍", "牒", "牖", "爰", "虢", "刖", "肟", "肜", "肓", "肼", "朊", "肽", "肱", "肫", 
            "肭", "肴", "肷", "胧", "胨", "胩", "胪", "胛", "胂", "胄", "胙", "胍", "胗", "朐", "胝", "胫", 
            "胱", "胴", "胭", "脍", "脎", "胲", "胼", "朕", "脒", "豚", "脶", "脞", "脬", "脘", "脲", "腈", 
            "腌", "腓", "腴", "腙", "腚", "腱", "腠", "腩", "腼", "腽", "腭", "腧", "塍", "媵", "膈", "膂", 
            "膑", "滕", "膣", "膪", "臌", "朦", "臊", "膻", "臁", "膦", "欤", "欷", "欹", "歃", "歆", "歙", 
            "飑", "飒", "飓", "飕", "飙", "飚", "殳", "彀", "毂", "觳", "斐", "齑", "斓", "於", "旆", "旄", 
            "旃", "旌", "旎", "旒", "旖", "炀", "炜", "炖", "炝", "炻", "烀", "炷", "炫", "炱", "烨", "烊", 
            "焐", "焓", "焖", "焯", "焱", "煳", "煜", "煨", "煅", "煲", "煊", "煸", "煺", "熘", "熳", "熵", 
            "熨", "熠", "燠", "燔", "燧", "燹", "爝", "爨", "灬", "焘", "煦", "熹", "戾", "戽", "扃", "扈", 
            "扉", "礻", "祀", "祆", "祉", "祛", "祜", "祓", "祚", "祢", "祗", "祠", "祯", "祧", "祺", "禅", 
            "禊", "禚", "禧", "禳", "忑", "忐", "怼", "恝", "恚", "恧", "恁", "恙", "恣", "悫", "愆", "愍", 
            "慝", "憩", "憝", "懋", "懑", "戆", "肀", "聿", "沓", "泶", "淼", "矶", "矸", "砀", "砉", "砗", 
            "砘", "砑", "斫", "砭", "砜", "砝", "砹", "砺", "砻", "砟", "砼", "砥", "砬", "砣", "砩", "硎", 
            "硭", "硖", "硗", "砦", "硐", "硇", "硌", "硪", "碛", "碓", "碚", "碇", "碜", "碡", "碣", "碲", 
            "碹", "碥", "磔", "磙", "磉", "磬", "磲", "礅", "磴", "礓", "礤", "礞", "礴", "龛", "黹", "黻", 
            "黼", "盱", "眄", "眍", "盹", "眇", "眈", "眚", "眢", "眙", "眭", "眦", "眵", "眸", "睐", "睑", 
            "睇", "睃", "睚", "睨", "睢", "睥", "睿", "瞍", "睽", "瞀", "瞌", "瞑", "瞟", "瞠", "瞰", "瞵", 
            "瞽", "町", "畀", "畎", "畋", "畈", "畛", "畲", "畹", "疃", "罘", "罡", "罟", "詈", "罨", "罴", 
            "罱", "罹", "羁", "罾", "盍", "盥", "蠲", "钅", "钆", "钇", "钋", "钊", "钌", "钍", "钏", "钐", 
            "钔", "钗", "钕", "钚", "钛", "钜", "钣", "钤", "钫", "钪", "钭", "钬", "钯", "钰", "钲", "钴", 
            "钶", "钷", "钸", "钹", "钺", "钼", "钽", "钿", "铄", "铈", "铉", "铊", "铋", "铌", "铍", "铎", 
            "铐", "铑", "铒", "铕", "铖", "铗", "铙", "铘", "铛", "铞", "铟", "铠", "铢", "铤", "铥", "铧", 
            "铨", "铪", "铩", "铫", "铮", "铯", "铳", "铴", "铵", "铷", "铹", "铼", "铽", "铿", "锃", "锂", 
            "锆", "锇", "锉", "锊", "锍", "锎", "锏", "锒", "锓", "锔", "锕", "锖", "锘", "锛", "锝", "锞", 
            "锟", "锢", "锪", "锫", "锩", "锬", "锱", "锲", "锴", "锶", "锷", "锸", "锼", "锾", "锿", "镂", 
            "锵", "镄", "镅", "镆", "镉", "镌", "镎", "镏", "镒", "镓", "镔", "镖", "镗", "镘", "镙", "镛", 
            "镞", "镟", "镝", "镡", "镢", "镤", "镥", "镦", "镧", "镨", "镩", "镪", "镫", "镬", "镯", "镱", 
            "镲", "镳", "锺", "矧", "矬", "雉", "秕", "秭", "秣", "秫", "稆", "嵇", "稃", "稂", "稞", "稔", 
            "稹", "稷", "穑", "黏", "馥", "穰", "皈", "皎", "皓", "皙", "皤", "瓞", "瓠", "甬", "鸠", "鸢", 
            "鸨", "鸩", "鸪", "鸫", "鸬", "鸲", "鸱", "鸶", "鸸", "鸷", "鸹", "鸺", "鸾", "鹁", "鹂", "鹄", 
            "鹆", "鹇", "鹈", "鹉", "鹋", "鹌", "鹎", "鹑", "鹕", "鹗", "鹚", "鹛", "鹜", "鹞", "鹣", "鹦", 
            "鹧", "鹨", "鹩", "鹪", "鹫", "鹬", "鹱", "鹭", "鹳", "疒", "疔", "疖", "疠", "疝", "疬", "疣", 
            "疳", "疴", "疸", "痄", "疱", "疰", "痃", "痂", "痖", "痍", "痣", "痨", "痦", "痤", "痫", "痧", 
            "瘃", "痱", "痼", "痿", "瘐", "瘀", "瘅", "瘌", "瘗", "瘊", "瘥", "瘘", "瘕", "瘙", "瘛", "瘼", 
            "瘢", "瘠", "癀", "瘭", "瘰", "瘿", "瘵", "癃", "瘾", "瘳", "癍", "癞", "癔", "癜", "癖", "癫", 
            "癯", "翊", "竦", "穸", "穹", "窀", "窆", "窈", "窕", "窦", "窠", "窬", "窨", "窭", "窳", "衤", 
            "衩", "衲", "衽", "衿", "袂", "袢", "裆", "袷", "袼", "裉", "裢", "裎", "裣", "裥", "裱", "褚", 
            "裼", "裨", "裾", "裰", "褡", "褙", "褓", "褛", "褊", "褴", "褫", "褶", "襁", "襦", "襻", "疋", 
            "胥", "皲", "皴", "矜", "耒", "耔", "耖", "耜", "耠", "耢", "耥", "耦", "耧", "耩", "耨", "耱", 
            "耋", "耵", "聃", "聆", "聍", "聒", "聩", "聱", "覃", "顸", "颀", "颃", "颉", "颌", "颍", "颏", 
            "颔", "颚", "颛", "颞", "颟", "颡", "颢", "颥", "颦", "虍", "虔", "虬", "虮", "虿", "虺", "虼", 
            "虻", "蚨", "蚍", "蚋", "蚬", "蚝", "蚧", "蚣", "蚪", "蚓", "蚩", "蚶", "蛄", "蚵", "蛎", "蚰", 
            "蚺", "蚱", "蚯", "蛉", "蛏", "蚴", "蛩", "蛱", "蛲", "蛭", "蛳", "蛐", "蜓", "蛞", "蛴", "蛟", 
            "蛘", "蛑", "蜃", "蜇", "蛸", "蜈", "蜊", "蜍", "蜉", "蜣", "蜻", "蜞", "蜥", "蜮", "蜚", "蜾", 
            "蝈", "蜴", "蜱", "蜩", "蜷", "蜿", "螂", "蜢", "蝽", "蝾", "蝻", "蝠", "蝰", "蝌", "蝮", "螋", 
            "蝓", "蝣", "蝼", "蝤", "蝙", "蝥", "螓", "螯", "螨", "蟒", "蟆", "螈", "螅", "螭", "螗", "螃", 
            "螫", "蟥", "螬", "螵", "螳", "蟋", "蟓", "螽", "蟑", "蟀", "蟊", "蟛", "蟪", "蟠", "蟮", "蠖", 
            "蠓", "蟾", "蠊", "蠛", "蠡", "蠹", "蠼", "缶", "罂", "罄", "罅", "舐", "竺", "竽", "笈", "笃", 
            "笄", "笕", "笊", "笫", "笏", "筇", "笸", "笪", "笙", "笮", "笱", "笠", "笥", "笤", "笳", "笾", 
            "笞", "筘", "筚", "筅", "筵", "筌", "筝", "筠", "筮", "筻", "筢", "筲", "筱", "箐", "箦", "箧", 
            "箸", "箬", "箝", "箨", "箅", "箪", "箜", "箢", "箫", "箴", "篑", "篁", "篌", "篝", "篚", "篥", 
            "篦", "篪", "簌", "篾", "篼", "簏", "簖", "簋", "簟", "簪", "簦", "簸", "籁", "籀", "臾", "舁", 
            "舂", "舄", "臬", "衄", "舡", "舢", "舣", "舭", "舯", "舨", "舫", "舸", "舻", "舳", "舴", "舾", 
            "艄", "艉", "艋", "艏", "艚", "艟", "艨", "衾", "袅", "袈", "裘", "裟", "襞", "羝", "羟", "羧", 
            "羯", "羰", "羲", "籼", "敉", "粑", "粝", "粜", "粞", "粢", "粲", "粼", "粽", "糁", "糇", "糌", 
            "糍", "糈", "糅", "糗", "糨", "艮", "暨", "羿", "翎", "翕", "翥", "翡", "翦", "翩", "翮", "翳", 
            "糸", "絷", "綦", "綮", "繇", "纛", "麸", "麴", "赳", "趄", "趔", "趑", "趱", "赧", "赭", "豇", 
            "豉", "酊", "酐", "酎", "酏", "酤", "酢", "酡", "酰", "酩", "酯", "酽", "酾", "酲", "酴", "酹", 
            "醌", "醅", "醐", "醍", "醑", "醢", "醣", "醪", "醭", "醮", "醯", "醵", "醴", "醺", "豕", "鹾", 
            "趸", "跫", "踅", "蹙", "蹩", "趵", "趿", "趼", "趺", "跄", "跖", "跗", "跚", "跞", "跎", "跏", 
            "跛", "跆", "跬", "跷", "跸", "跣", "跹", "跻", "跤", "踉", "跽", "踔", "踝", "踟", "踬", "踮", 
            "踣", "踯", "踺", "蹀", "踹", "踵", "踽", "踱", "蹉", "蹁", "蹂", "蹑", "蹒", "蹊", "蹰", "蹶", 
            "蹼", "蹯", "蹴", "躅", "躏", "躔", "躐", "躜", "躞", "豸", "貂", "貊", "貅", "貘", "貔", "斛", 
            "觖", "觞", "觚", "觜", "觥", "觫", "觯", "訾", "謦", "靓", "雩", "雳", "雯", "霆", "霁", "霈", 
            "霏", "霎", "霪", "霭", "霰", "霾", "龀", "龃", "龅", "龆", "龇", "龈", "龉", "龊", "龌", "黾", 
            "鼋", "鼍", "隹", "隼", "隽", "雎", "雒", "瞿", "雠", "銎", "銮", "鋈", "錾", "鍪", "鏊", "鎏", 
            "鐾", "鑫", "鱿", "鲂", "鲅", "鲆", "鲇", "鲈", "稣", "鲋", "鲎", "鲐", "鲑", "鲒", "鲔", "鲕", 
            "鲚", "鲛", "鲞", "鲟", "鲠", "鲡", "鲢", "鲣", "鲥", "鲦", "鲧", "鲨", "鲩", "鲫", "鲭", "鲮", 
            "鲰", "鲱", "鲲", "鲳", "鲴", "鲵", "鲶", "鲷", "鲺", "鲻", "鲼", "鲽", "鳄", "鳅", "鳆", "鳇", 
            "鳊", "鳋", "鳌", "鳍", "鳎", "鳏", "鳐", "鳓", "鳔", "鳕", "鳗", "鳘", "鳙", "鳜", "鳝", "鳟", 
            "鳢", "靼", "鞅", "鞑", "鞒", "鞔", "鞯", "鞫", "鞣", "鞲", "鞴", "骱", "骰", "骷", "鹘", "骶", 
            "骺", "骼", "髁", "髀", "髅", "髂", "髋", "髌", "髑", "魅", "魃", "魇", "魉", "魈", "魍", "魑", 
            "飨", "餍", "餮", "饕", "饔", "髟", "髡", "髦", "髯", "髫", "髻", "髭", "髹", "鬈", "鬏", "鬓", 
            "鬟", "鬣", "麽", "麾", "縻", "麂", "麇", "麈", "麋", "麒", "鏖", "麝", "麟", "黛", "黜", "黝", 
            "黠", "黟", "黢", "黩", "黧", "黥", "黪", "黯", "鼢", "鼬", "鼯", "鼹", "鼷", "鼽", "鼾", "齄"
         };
        public static string[] otherPinYin = new string[] { 
            "Chu", "Ji", "Wu", "Gai", "Nian", "Sa", "Pi", "Gen", "Cheng", "Ge", "Nao", "E", "Shu", "Yu", "Pie", "Bi", 
            "Tuo", "Yao", "Yao", "Zhi", "Di", "Xin", "Yin", "Kui", "Yu", "Gao", "Tao", "Dian", "Ji", "Nai", "Nie", "Ji", 
            "Qi", "Mi", "Bei", "Se", "Gu", "Ze", "She", "Cuo", "Yan", "Jue", "Si", "Ye", "Yan", "Fang", "Po", "Gui", 
            "Kui", "Bian", "Ze", "Gua", "You", "Ce", "Yi", "Wen", "Jing", "Ku", "Gui", "Kai", "La", "Ji", "Yan", "Wan", 
            "Kuai", "Piao", "Jue", "Qiao", "Huo", "Yi", "Tong", "Wang", "Dan", "Ding", "Zhang", "Le", "Sa", "Yi", "Mu", "Ren", 
            "Yu", "Pi", "Ya", "Wa", "Wu", "Chang", "Cang", "Kang", "Zhu", "Ning", "Ka", "You", "Yi", "Gou", "Tong", "Tuo", 
            "Ni", "Ga", "Ji", "Er", "You", "Kua", "Kan", "Zhu", "Yi", "Tiao", "Chai", "Jiao", "Nong", "Mou", "Chou", "Yan", 
            "Li", "Qiu", "Li", "Yu", "Ping", "Yong", "Si", "Feng", "Qian", "Ruo", "Pai", "Zhuo", "Shu", "Luo", "Wo", "Bi", 
            "Ti", "Guan", "Kong", "Ju", "Fen", "Yan", "Xie", "Ji", "Wei", "Zong", "Lou", "Tang", "Bin", "Nuo", "Chi", "Xi", 
            "Jing", "Jian", "Jiao", "Jiu", "Tong", "Xuan", "Dan", "Tong", "Tun", "She", "Qian", "Zu", "Yue", "Cuan", "Di", "Xi", 
            "Xun", "Hong", "Guo", "Chan", "Kui", "Bao", "Pu", "Hong", "Fu", "Fu", "Su", "Si", "Wen", "Yan", "Bo", "Gun", 
            "Mao", "Xie", "Luan", "Pou", "Bing", "Ying", "Luo", "Lei", "Liang", "Hu", "Lie", "Xian", "Song", "Ping", "Zhong", "Ming", 
            "Yan", "Jie", "Hong", "Shan", "Ou", "Ju", "Ne", "Gu", "He", "Di", "Zhao", "Qu", "Dai", "Kuang", "Lei", "Gua", 
            "Jie", "Hui", "Shen", "Gou", "Quan", "Zheng", "Hun", "Xu", "Qiao", "Gao", "Kuang", "Ei", "Zou", "Zhuo", "Wei", "Yu", 
            "Shen", "Chan", "Sui", "Chen", "Jian", "Xue", "Ye", "E", "Yu", "Xuan", "An", "Di", "Zi", "Pian", "Mo", "Dang", 
            "Su", "Shi", "Mi", "Zhe", "Jian", "Zen", "Qiao", "Jue", "Yan", "Zhan", "Chen", "Dan", "Jin", "Zuo", "Wu", "Qian", 
            "Jing", "Ban", "Yan", "Zuo", "Bei", "Jing", "Gai", "Zhi", "Nie", "Zou", "Chui", "Pi", "Wei", "Huang", "Wei", "Xi", 
            "Han", "Qiong", "Kuang", "Mang", "Wu", "Fang", "Bing", "Pi", "Bei", "Ye", "Di", "Tai", "Jia", "Zhi", "Zhu", "Kuai", 
            "Qie", "Xun", "Yun", "Li", "Ying", "Gao", "Xi", "Fu", "Pi", "Tan", "Yan", "Juan", "Yan", "Yin", "Zhang", "Po", 
            "Shan", "Zou", "Ling", "Feng", "Chu", "Huan", "Mai", "Qu", "Shao", "He", "Ge", "Meng", "Xu", "Xie", "Sou", "Xie", 
            "Jue", "Jian", "Qian", "Dang", "Chang", "Si", "Bian", "Ben", "Qiu", "Ben", "E", "Fa", "Shu", "Ji", "Yong", "He", 
            "Wei", "Wu", "Ge", "Zhen", "Kuang", "Pi", "Yi", "Li", "Qi", "Ban", "Gan", "Long", "Dian", "Lu", "Che", "Di", 
            "Tuo", "Ni", "Mu", "Ao", "Ya", "Die", "Dong", "Kai", "Shan", "Shang", "Nao", "Gai", "Yin", "Cheng", "Shi", "Guo", 
            "Xun", "Lie", "Yuan", "Zhi", "An", "Yi", "Pi", "Nian", "Peng", "Tu", "Sao", "Dai", "Ku", "Die", "Yin", "Leng", 
            "Hou", "Ge", "Yuan", "Man", "Yong", "Liang", "Chi", "Xin", "Pi", "Yi", "Cao", "Jiao", "Nai", "Du", "Qian", "Ji", 
            "Wan", "Xiong", "Qi", "Xiang", "Fu", "Yuan", "Yun", "Fei", "Ji", "Li", "E", "Ju", "Pi", "Zhi", "Rui", "Xian", 
            "Chang", "Cong", "Qin", "Wu", "Qian", "Qi", "Shan", "Bian", "Zhu", "Kou", "Yi", "Mo", "Gan", "Pie", "Long", "Ba", 
            "Mu", "Ju", "Ran", "Qing", "Chi", "Fu", "Ling", "Niao", "Yin", "Mao", "Ying", "Qiong", "Min", "Tiao", "Qian", "Yi", 
            "Rao", "Bi", "Zi", "Ju", "Tong", "Hui", "Zhu", "Ting", "Qiao", "Fu", "Ren", "Xing", "Quan", "Hui", "Xun", "Ming", 
            "Qi", "Jiao", "Chong", "Jiang", "Luo", "Ying", "Qian", "Gen", "Jin", "Mai", "Sun", "Hong", "Zhou", "Kan", "Bi", "Shi", 
            "Wo", "You", "E", "Mei", "You", "Li", "Tu", "Xian", "Fu", "Sui", "You", "Di", "Shen", "Guan", "Lang", "Ying", 
            "Chun", "Jing", "Qi", "Xi", "Song", "Jin", "Nai", "Qi", "Ba", "Shu", "Chang", "Tie", "Yu", "Huan", "Bi", "Fu", 
            "Tu", "Dan", "Cui", "Yan", "Zu", "Dang", "Jian", "Wan", "Ying", "Gu", "Han", "Qia", "Feng", "Shen", "Xiang", "Wei", 
            "Chan", "Kai", "Qi", "Kui", "Xi", "E", "Bao", "Pa", "Ting", "Lou", "Pai", "Xuan", "Jia", "Zhen", "Shi", "Ru", 
            "Mo", "En", "Bei", "Weng", "Hao", "Ji", "Li", "Bang", "Jian", "Shuo", "Lang", "Ying", "Yu", "Su", "Meng", "Dou", 
            "Xi", "Lian", "Cu", "Lin", "Qu", "Kou", "Xu", "Liao", "Hui", "Xun", "Jue", "Rui", "Zui", "Ji", "Meng", "Fan", 
            "Qi", "Hong", "Xie", "Hong", "Wei", "Yi", "Weng", "Sou", "Bi", "Hao", "Tai", "Ru", "Xun", "Xian", "Gao", "Li", 
            "Huo", "Qu", "Heng", "Fan", "Nie", "Mi", "Gong", "Yi", "Kuang", "Lian", "Da", "Yi", "Xi", "Zang", "Pao", "You", 
            "Liao", "Ga", "Gan", "Ti", "Men", "Tuan", "Chen", "Fu", "Pin", "Niu", "Jie", "Jiao", "Za", "Yi", "Lv", "Jun", 
            "Tian", "Ye", "Ai", "Na", "Ji", "Guo", "Bai", "Ju", "Pou", "Lie", "Qian", "Guan", "Die", "Zha", "Ya", "Qin", 
            "Yu", "An", "Xuan", "Bing", "Kui", "Yuan", "Shu", "En", "Chuai", "Jian", "Shuo", "Zhan", "Nuo", "Sang", "Luo", "Ying", 
            "Zhi", "Han", "Zhe", "Xie", "Lu", "Zun", "Cuan", "Gan", "Huan", "Pi", "Xing", "Zhuo", "Huo", "Zuan", "Nang", "Yi", 
            "Te", "Dai", "Shi", "Bu", "Chi", "Ji", "Kou", "Dao", "Le", "Zha", "A", "Yao", "Fu", "Mu", "Yi", "Tai", 
            "Li", "E", "Bi", "Bei", "Guo", "Qin", "Yin", "Za", "Ka", "Ga", "Gua", "Ling", "Dong", "Ning", "Duo", "Nao", 
            "You", "Si", "Kuang", "Ji", "Shen", "Hui", "Da", "Lie", "Yi", "Xiao", "Bi", "Ci", "Guang", "Yue", "Xiu", "Yi", 
            "Pai", "Kuai", "Duo", "Ji", "Mie", "Mi", "Zha", "Nong", "Gen", "Mou", "Mai", "Chi", "Lao", "Geng", "En", "Zha", 
            "Suo", "Zao", "Xi", "Zuo", "Ji", "Feng", "Ze", "Nuo", "Miao", "Lin", "Zhuan", "Zhou", "Tao", "Hu", "Cui", "Sha", 
            "Yo", "Dan", "Bo", "Ding", "Lang", "Li", "Shua", "Chuo", "Die", "Da", "Nan", "Li", "Kui", "Jie", "Yong", "Kui", 
            "Jiu", "Sou", "Yin", "Chi", "Jie", "Lou", "Ku", "Wo", "Hui", "Qin", "Ao", "Su", "Du", "Ke", "Nie", "He", 
            "Chen", "Suo", "Ge", "A", "En", "Hao", "Dia", "Ai", "Ai", "Suo", "Hei", "Tong", "Chi", "Pei", "Lei", "Cao", 
            "Piao", "Qi", "Ying", "Beng", "Sou", "Di", "Mi", "Peng", "Jue", "Liao", "Pu", "Chuai", "Jiao", "O", "Qin", "Lu", 
            "Ceng", "Deng", "Hao", "Jin", "Jue", "Yi", "Sai", "Pi", "Ru", "Cha", "Huo", "Nang", "Wei", "Jian", "Nan", "Lun", 
            "Hu", "Ling", "You", "Yu", "Qing", "Yu", "Huan", "Wei", "Zhi", "Pei", "Tang", "Dao", "Ze", "Guo", "Wei", "Wo", 
            "Man", "Zhang", "Fu", "Fan", "Ji", "Qi", "Qian", "Qi", "Qu", "Ya", "Xian", "Ao", "Cen", "Lan", "Ba", "Hu", 
            "Ke", "Dong", "Jia", "Xiu", "Dai", "Gou", "Mao", "Min", "Yi", "Dong", "Qiao", "Xun", "Zheng", "Lao", "Lai", "Song", 
            "Yan", "Gu", "Xiao", "Guo", "Kong", "Jue", "Rong", "Yao", "Wai", "Zai", "Wei", "Yu", "Cuo", "Lou", "Zi", "Mei", 
            "Sheng", "Song", "Ji", "Zhang", "Lin", "Deng", "Bin", "Yi", "Dian", "Chi", "Pang", "Cu", "Xun", "Yang", "Hou", "Lai", 
            "Xi", "Chang", "Huang", "Yao", "Zheng", "Jiao", "Qu", "San", "Fan", "Qiu", "An", "Guang", "Ma", "Niu", "Yun", "Xia", 
            "Pao", "Fei", "Rong", "Kuai", "Shou", "Sun", "Bi", "Juan", "Li", "Yu", "Xian", "Yin", "Suan", "Yi", "Guo", "Luo", 
            "Ni", "She", "Cu", "Mi", "Hu", "Cha", "Wei", "Wei", "Mei", "Nao", "Zhang", "Jing", "Jue", "Liao", "Xie", "Xun", 
            "Huan", "Chuan", "Huo", "Sun", "Yin", "Dong", "Shi", "Tang", "Tun", "Xi", "Ren", "Yu", "Chi", "Yi", "Xiang", "Bo", 
            "Yu", "Hun", "Zha", "Sou", "Mo", "Xiu", "Jin", "San", "Zhuan", "Nang", "Pi", "Wu", "Gui", "Pao", "Xiu", "Xiang", 
            "Tuo", "An", "Yu", "Bi", "Geng", "Ao", "Jin", "Chan", "Xie", "Lin", "Ying", "Shu", "Dao", "Cun", "Chan", "Wu", 
            "Zhi", "Ou", "Chong", "Wu", "Kai", "Chang", "Chuang", "Song", "Bian", "Niu", "Hu", "Chu", "Peng", "Da", "Yang", "Zuo", 
            "Ni", "Fu", "Chao", "Yi", "Yi", "Tong", "Yan", "Ce", "Kai", "Xun", "Ke", "Yun", "Bei", "Song", "Qian", "Kui", 
            "Kun", "Yi", "Ti", "Quan", "Qie", "Xing", "Fei", "Chang", "Wang", "Chou", "Hu", "Cui", "Yun", "Kui", "E", "Leng", 
            "Zhui", "Qiao", "Bi", "Su", "Qie", "Yong", "Jing", "Qiao", "Chong", "Chu", "Lin", "Meng", "Tian", "Hui", "Shuan", "Yan", 
            "Wei", "Hong", "Min", "Kang", "Ta", "Lv", "Kun", "Jiu", "Lang", "Yu", "Chang", "Xi", "Wen", "Hun", "E", "Qu", 
            "Que", "He", "Tian", "Que", "Kan", "Jiang", "Pan", "Qiang", "San", "Qi", "Si", "Cha", "Feng", "Yuan", "Mu", "Mian", 
            "Dun", "Mi", "Gu", "Bian", "Wen", "Hang", "Wei", "Le", "Gan", "Shu", "Long", "Lu", "Yang", "Si", "Duo", "Ling", 
            "Mao", "Luo", "Xuan", "Pan", "Duo", "Hong", "Min", "Jing", "Huan", "Wei", "Lie", "Jia", "Zhen", "Yin", "Hui", "Zhu", 
            "Ji", "Xu", "Hui", "Tao", "Xun", "Jiang", "Liu", "Hu", "Xun", "Ru", "Su", "Wu", "Lai", "Wei", "Zhuo", "Juan", 
            "Cen", "Bang", "Xi", "Mei", "Huan", "Zhu", "Qi", "Xi", "Song", "Du", "Zhuo", "Pei", "Mian", "Gan", "Fei", "Cong", 
            "Shen", "Guan", "Lu", "Shuan", "Xie", "Yan", "Mian", "Qiu", "Sou", "Huang", "Xu", "Pen", "Jian", "Xuan", "Wo", "Mei", 
            "Yan", "Qin", "Ke", "She", "Mang", "Ying", "Pu", "Li", "Ru", "Ta", "Hun", "Bi", "Xiu", "Fu", "Tang", "Pang", 
            "Ming", "Huang", "Ying", "Xiao", "Lan", "Cao", "Hu", "Luo", "Huan", "Lian", "Zhu", "Yi", "Lu", "Xuan", "Gan", "Shu", 
            "Si", "Shan", "Shao", "Tong", "Chan", "Lai", "Sui", "Li", "Dan", "Chan", "Lian", "Ru", "Pu", "Bi", "Hao", "Zhuo", 
            "Han", "Xie", "Ying", "Yue", "Fen", "Hao", "Ba", "Bao", "Gui", "Dang", "Mi", "You", "Chen", "Ning", "Jian", "Qian", 
            "Wu", "Liao", "Qian", "Huan", "Jian", "Jian", "Zou", "Ya", "Wu", "Jiong", "Ze", "Yi", "Er", "Jia", "Jing", "Dai", 
            "Hou", "Pang", "Bu", "Li", "Qiu", "Xiao", "Ti", "Qun", "Kui", "Wei", "Huan", "Lu", "Chuan", "Huang", "Qiu", "Xia", 
            "Ao", "Gou", "Ta", "Liu", "Xian", "Lin", "Ju", "Xie", "Miao", "Sui", "La", "Ji", "Hui", "Tuan", "Zhi", "Kao", 
            "Zhi", "Ji", "E", "Chan", "Xi", "Ju", "Chan", "Jing", "Nu", "Mi", "Fu", "Bi", "Yu", "Che", "Shuo", "Fei", 
            "Yan", "Wu", "Yu", "Bi", "Jin", "Zi", "Gui", "Niu", "Yu", "Si", "Da", "Zhou", "Shan", "Qie", "Ya", "Rao", 
            "Shu", "Luan", "Jiao", "Pin", "Cha", "Li", "Ping", "Wa", "Xian", "Suo", "Di", "Wei", "E", "Jing", "Biao", "Jie", 
            "Chang", "Bi", "Chan", "Nu", "Ao", "Yuan", "Ting", "Wu", "Gou", "Mo", "Pi", "Ai", "Pin", "Chi", "Li", "Yan", 
            "Qiang", "Piao", "Chang", "Lei", "Zhang", "Xi", "Shan", "Bi", "Niao", "Mo", "Shuang", "Ga", "Ga", "Fu", "Nu", "Zi", 
            "Jie", "Jue", "Bao", "Zang", "Si", "Fu", "Zou", "Yi", "Nu", "Dai", "Xiao", "Hua", "Pian", "Li", "Qi", "Ke", 
            "Zhui", "Can", "Zhi", "Wu", "Ao", "Liu", "Shan", "Biao", "Cong", "Chan", "Ji", "Xiang", "Jiao", "Yu", "Zhou", "Ge", 
            "Wan", "Kuang", "Yun", "Pi", "Shu", "Gan", "Xie", "Fu", "Zhou", "Fu", "Chu", "Dai", "Ku", "Hang", "Jiang", "Geng", 
            "Xiao", "Ti", "Ling", "Qi", "Fei", "Shang", "Gun", "Duo", "Shou", "Liu", "Quan", "Wan", "Zi", "Ke", "Xiang", "Ti", 
            "Miao", "Hui", "Si", "Bian", "Gou", "Zhui", "Min", "Jin", "Zhen", "Ru", "Gao", "Li", "Yi", "Jian", "Bin", "Piao", 
            "Man", "Lei", "Miao", "Sao", "Xie", "Liao", "Zeng", "Jiang", "Qian", "Qiao", "Huan", "Zuan", "Yao", "Ji", "Chuan", "Zai", 
            "Yong", "Ding", "Ji", "Wei", "Bin", "Min", "Jue", "Ke", "Long", "Dian", "Dai", "Po", "Min", "Jia", "Er", "Gong", 
            "Xu", "Ya", "Heng", "Yao", "Luo", "Xi", "Hui", "Lian", "Qi", "Ying", "Qi", "Hu", "Kun", "Yan", "Cong", "Wan", 
            "Chen", "Ju", "Mao", "Yu", "Yuan", "Xia", "Nao", "Ai", "Tang", "Jin", "Huang", "Ying", "Cui", "Cong", "Xuan", "Zhang", 
            "Pu", "Can", "Qu", "Lu", "Bi", "Zan", "Wen", "Wei", "Yun", "Tao", "Wu", "Shao", "Qi", "Cha", "Ma", "Li", 
            "Pi", "Miao", "Yao", "Rui", "Jian", "Chu", "Cheng", "Cong", "Xiao", "Fang", "Pa", "Zhu", "Nai", "Zhi", "Zhe", "Long", 
            "Jiu", "Ping", "Lu", "Xia", "Xiao", "You", "Zhi", "Tuo", "Zhi", "Ling", "Gou", "Di", "Li", "Tuo", "Cheng", "Kao", 
            "Lao", "Ya", "Rao", "Zhi", "Zhen", "Guang", "Qi", "Ting", "Gua", "Jiu", "Hua", "Heng", "Gui", "Jie", "Luan", "Juan", 
            "An", "Xu", "Fan", "Gu", "Fu", "Jue", "Zi", "Suo", "Ling", "Chu", "Fen", "Du", "Qian", "Zhao", "Luo", "Chui", 
            "Liang", "Guo", "Jian", "Di", "Ju", "Cou", "Zhen", "Nan", "Zha", "Lian", "Lan", "Ji", "Pin", "Ju", "Qiu", "Duan", 
            "Chui", "Chen", "Lv", "Cha", "Ju", "Xuan", "Mei", "Ying", "Zhen", "Fei", "Ta", "Sun", "Xie", "Gao", "Cui", "Gao", 
            "Shuo", "Bin", "Rong", "Zhu", "Xie", "Jin", "Qiang", "Qi", "Chu", "Tang", "Zhu", "Hu", "Gan", "Yue", "Qing", "Tuo", 
            "Jue", "Qiao", "Qin", "Lu", "Zun", "Xi", "Ju", "Yuan", "Lei", "Yan", "Lin", "Bo", "Cha", "You", "Ao", "Mo", 
            "Cu", "Shang", "Tian", "Yun", "Lian", "Piao", "Dan", "Ji", "Bin", "Yi", "Ren", "E", "Gu", "Ke", "Lu", "Zhi", 
            "Yi", "Zhen", "Hu", "Li", "Yao", "Shi", "Zhi", "Quan", "Lu", "Zhe", "Nian", "Wang", "Chuo", "Zi", "Cou", "Lu", 
            "Lin", "Wei", "Jian", "Qiang", "Jia", "Ji", "Ji", "Kan", "Deng", "Gai", "Jian", "Zang", "Ou", "Ling", "Bu", "Beng", 
            "Zeng", "Pi", "Po", "Ga", "La", "Gan", "Hao", "Tan", "Gao", "Ze", "Xin", "Yun", "Gui", "He", "Zan", "Mao", 
            "Yu", "Chang", "Ni", "Qi", "Sheng", "Ye", "Chao", "Yan", "Hui", "Bu", "Han", "Gui", "Xuan", "Kui", "Ai", "Ming", 
            "Tun", "Xun", "Yao", "Xi", "Nang", "Ben", "Shi", "Kuang", "Yi", "Zhi", "Zi", "Gai", "Jin", "Zhen", "Lai", "Qiu", 
            "Ji", "Dan", "Fu", "Chan", "Ji", "Xi", "Di", "Yu", "Gou", "Jin", "Qu", "Jian", "Jiang", "Pin", "Mao", "Gu", 
            "Wu", "Gu", "Ji", "Ju", "Jian", "Pian", "Kao", "Qie", "Suo", "Bai", "Ge", "Bo", "Mao", "Mu", "Cui", "Jian", 
            "San", "Shu", "Chang", "Lu", "Pu", "Qu", "Pie", "Dao", "Xian", "Chuan", "Dong", "Ya", "Yin", "Ke", "Yun", "Fan", 
            "Chi", "Jiao", "Du", "Die", "You", "Yuan", "Guo", "Yue", "Wo", "Rong", "Huang", "Jing", "Ruan", "Tai", "Gong", "Zhun", 
            "Na", "Yao", "Qian", "Long", "Dong", "Ka", "Lu", "Jia", "Shen", "Zhou", "Zuo", "Gua", "Zhen", "Qu", "Zhi", "Jing", 
            "Guang", "Dong", "Yan", "Kuai", "Sa", "Hai", "Pian", "Zhen", "Mi", "Tun", "Luo", "Cuo", "Pao", "Wan", "Niao", "Jing", 
            "Yan", "Fei", "Yu", "Zong", "Ding", "Jian", "Cou", "Nan", "Mian", "Wa", "E", "Shu", "Cheng", "Ying", "Ge", "Lv", 
            "Bin", "Teng", "Zhi", "Chuai", "Gu", "Meng", "Sao", "Shan", "Lian", "Lin", "Yu", "Xi", "Qi", "Sha", "Xin", "Xi", 
            "Biao", "Sa", "Ju", "Sou", "Biao", "Biao", "Shu", "Gou", "Gu", "Hu", "Fei", "Ji", "Lan", "Yu", "Pei", "Mao", 
            "Zhan", "Jing", "Ni", "Liu", "Yi", "Yang", "Wei", "Dun", "Qiang", "Shi", "Hu", "Zhu", "Xuan", "Tai", "Ye", "Yang", 
            "Wu", "Han", "Men", "Chao", "Yan", "Hu", "Yu", "Wei", "Duan", "Bao", "Xuan", "Bian", "Tui", "Liu", "Man", "Shang", 
            "Yun", "Yi", "Yu", "Fan", "Sui", "Xian", "Jue", "Cuan", "Huo", "Tao", "Xu", "Xi", "Li", "Hu", "Jiong", "Hu", 
            "Fei", "Shi", "Si", "Xian", "Zhi", "Qu", "Hu", "Fu", "Zuo", "Mi", "Zhi", "Ci", "Zhen", "Tiao", "Qi", "Chan", 
            "Xi", "Zhuo", "Xi", "Rang", "Te", "Tan", "Dui", "Jia", "Hui", "Nv", "Nin", "Yang", "Zi", "Que", "Qian", "Min", 
            "Te", "Qi", "Dui", "Mao", "Men", "Gang", "Yu", "Yu", "Ta", "Xue", "Miao", "Ji", "Gan", "Dang", "Hua", "Che", 
            "Dun", "Ya", "Zhuo", "Bian", "Feng", "Fa", "Ai", "Li", "Long", "Zha", "Tong", "Di", "La", "Tuo", "Fu", "Xing", 
            "Mang", "Xia", "Qiao", "Zhai", "Dong", "Nao", "Ge", "Wo", "Qi", "Dui", "Bei", "Ding", "Chen", "Zhou", "Jie", "Di", 
            "Xuan", "Bian", "Zhe", "Gun", "Sang", "Qing", "Qu", "Dun", "Deng", "Jiang", "Ca", "Meng", "Bo", "Kan", "Zhi", "Fu", 
            "Fu", "Xu", "Mian", "Kou", "Dun", "Miao", "Dan", "Sheng", "Yuan", "Yi", "Sui", "Zi", "Chi", "Mou", "Lai", "Jian", 
            "Di", "Suo", "Ya", "Ni", "Sui", "Pi", "Rui", "Sou", "Kui", "Mao", "Ke", "Ming", "Piao", "Cheng", "Kan", "Lin", 
            "Gu", "Ding", "Bi", "Quan", "Tian", "Fan", "Zhen", "She", "Wan", "Tuan", "Fu", "Gang", "Gu", "Li", "Yan", "Pi", 
            "Lan", "Li", "Ji", "Zeng", "He", "Guan", "Juan", "Jin", "Ga", "Yi", "Po", "Zhao", "Liao", "Tu", "Chuan", "Shan", 
            "Men", "Chai", "Nv", "Bu", "Tai", "Ju", "Ban", "Qian", "Fang", "Kang", "Dou", "Huo", "Ba", "Yu", "Zheng", "Gu", 
            "Ke", "Po", "Bu", "Bo", "Yue", "Mu", "Tan", "Dian", "Shuo", "Shi", "Xuan", "Ta", "Bi", "Ni", "Pi", "Duo", 
            "Kao", "Lao", "Er", "You", "Cheng", "Jia", "Nao", "Ye", "Cheng", "Diao", "Yin", "Kai", "Zhu", "Ding", "Diu", "Hua", 
            "Quan", "Ha", "Sha", "Diao", "Zheng", "Se", "Chong", "Tang", "An", "Ru", "Lao", "Lai", "Te", "Keng", "Zeng", "Li", 
            "Gao", "E", "Cuo", "Lve", "Liu", "Kai", "Jian", "Lang", "Qin", "Ju", "A", "Qiang", "Nuo", "Ben", "De", "Ke", 
            "Kun", "Gu", "Huo", "Pei", "Juan", "Tan", "Zi", "Qie", "Kai", "Si", "E", "Cha", "Sou", "Huan", "Ai", "Lou", 
            "Qiang", "Fei", "Mei", "Mo", "Ge", "Juan", "Na", "Liu", "Yi", "Jia", "Bin", "Biao", "Tang", "Man", "Luo", "Yong", 
            "Chuo", "Xuan", "Di", "Tan", "Jue", "Pu", "Lu", "Dui", "Lan", "Pu", "Cuan", "Qiang", "Deng", "Huo", "Zhuo", "Yi", 
            "Cha", "Biao", "Zhong", "Shen", "Cuo", "Zhi", "Bi", "Zi", "Mo", "Shu", "Lv", "Ji", "Fu", "Lang", "Ke", "Ren", 
            "Zhen", "Ji", "Se", "Nian", "Fu", "Rang", "Gui", "Jiao", "Hao", "Xi", "Po", "Die", "Hu", "Yong", "Jiu", "Yuan", 
            "Bao", "Zhen", "Gu", "Dong", "Lu", "Qu", "Chi", "Si", "Er", "Zhi", "Gua", "Xiu", "Luan", "Bo", "Li", "Hu", 
            "Yu", "Xian", "Ti", "Wu", "Miao", "An", "Bei", "Chun", "Hu", "E", "Ci", "Mei", "Wu", "Yao", "Jian", "Ying", 
            "Zhe", "Liu", "Liao", "Jiao", "Jiu", "Yu", "Hu", "Lu", "Guan", "Bing", "Ding", "Jie", "Li", "Shan", "Li", "You", 
            "Gan", "Ke", "Da", "Zha", "Pao", "Zhu", "Xuan", "Jia", "Ya", "Yi", "Zhi", "Lao", "Wu", "Cuo", "Xian", "Sha", 
            "Zhu", "Fei", "Gu", "Wei", "Yu", "Yu", "Dan", "La", "Yi", "Hou", "Chai", "Lou", "Jia", "Sao", "Chi", "Mo", 
            "Ban", "Ji", "Huang", "Biao", "Luo", "Ying", "Zhai", "Long", "Yin", "Chou", "Ban", "Lai", "Yi", "Dian", "Pi", "Dian", 
            "Qu", "Yi", "Song", "Xi", "Qiong", "Zhun", "Bian", "Yao", "Tiao", "Dou", "Ke", "Yu", "Xun", "Ju", "Yu", "Yi", 
            "Cha", "Na", "Ren", "Jin", "Mei", "Pan", "Dang", "Jia", "Ge", "Ken", "Lian", "Cheng", "Lian", "Jian", "Biao", "Chu", 
            "Ti", "Bi", "Ju", "Duo", "Da", "Bei", "Bao", "Lv", "Bian", "Lan", "Chi", "Zhe", "Qiang", "Ru", "Pan", "Ya", 
            "Xu", "Jun", "Cun", "Jin", "Lei", "Zi", "Chao", "Si", "Huo", "Lao", "Tang", "Ou", "Lou", "Jiang", "Nou", "Mo", 
            "Die", "Ding", "Dan", "Ling", "Ning", "Guo", "Kui", "Ao", "Qin", "Han", "Qi", "Hang", "Jie", "He", "Ying", "Ke", 
            "Han", "E", "Zhuan", "Nie", "Man", "Sang", "Hao", "Ru", "Pin", "Hu", "Qian", "Qiu", "Ji", "Chai", "Hui", "Ge", 
            "Meng", "Fu", "Pi", "Rui", "Xian", "Hao", "Jie", "Gong", "Dou", "Yin", "Chi", "Han", "Gu", "Ke", "Li", "You", 
            "Ran", "Zha", "Qiu", "Ling", "Cheng", "You", "Qiong", "Jia", "Nao", "Zhi", "Si", "Qu", "Ting", "Kuo", "Qi", "Jiao", 
            "Yang", "Mou", "Shen", "Zhe", "Shao", "Wu", "Li", "Chu", "Fu", "Qiang", "Qing", "Qi", "Xi", "Yu", "Fei", "Guo", 
            "Guo", "Yi", "Pi", "Tiao", "Quan", "Wan", "Lang", "Meng", "Chun", "Rong", "Nan", "Fu", "Kui", "Ke", "Fu", "Sou", 
            "Yu", "You", "Lou", "You", "Bian", "Mou", "Qin", "Ao", "Man", "Mang", "Ma", "Yuan", "Xi", "Chi", "Tang", "Pang", 
            "Shi", "Huang", "Cao", "Piao", "Tang", "Xi", "Xiang", "Zhong", "Zhang", "Shuai", "Mao", "Peng", "Hui", "Pan", "Shan", "Huo", 
            "Meng", "Chan", "Lian", "Mie", "Li", "Du", "Qu", "Fou", "Ying", "Qing", "Xia", "Shi", "Zhu", "Yu", "Ji", "Du", 
            "Ji", "Jian", "Zhao", "Zi", "Hu", "Qiong", "Po", "Da", "Sheng", "Ze", "Gou", "Li", "Si", "Tiao", "Jia", "Bian", 
            "Chi", "Kou", "Bi", "Xian", "Yan", "Quan", "Zheng", "Jun", "Shi", "Gang", "Pa", "Shao", "Xiao", "Qing", "Ze", "Qie", 
            "Zhu", "Ruo", "Qian", "Tuo", "Bi", "Dan", "Kong", "Wan", "Xiao", "Zhen", "Kui", "Huang", "Hou", "Gou", "Fei", "Li", 
            "Bi", "Chi", "Su", "Mie", "Dou", "Lu", "Duan", "Gui", "Dian", "Zan", "Deng", "Bo", "Lai", "Zhou", "Yu", "Yu", 
            "Chong", "Xi", "Nie", "Nv", "Chuan", "Shan", "Yi", "Bi", "Zhong", "Ban", "Fang", "Ge", "Lu", "Zhu", "Ze", "Xi", 
            "Shao", "Wei", "Meng", "Shou", "Cao", "Chong", "Meng", "Qin", "Niao", "Jia", "Qiu", "Sha", "Bi", "Di", "Qiang", "Suo", 
            "Jie", "Tang", "Xi", "Xian", "Mi", "Ba", "Li", "Tiao", "Xi", "Zi", "Can", "Lin", "Zong", "San", "Hou", "Zan", 
            "Ci", "Xu", "Rou", "Qiu", "Jiang", "Gen", "Ji", "Yi", "Ling", "Xi", "Zhu", "Fei", "Jian", "Pian", "He", "Yi", 
            "Jiao", "Zhi", "Qi", "Qi", "Yao", "Dao", "Fu", "Qu", "Jiu", "Ju", "Lie", "Zi", "Zan", "Nan", "Zhe", "Jiang", 
            "Chi", "Ding", "Gan", "Zhou", "Yi", "Gu", "Zuo", "Tuo", "Xian", "Ming", "Zhi", "Yan", "Shai", "Cheng", "Tu", "Lei", 
            "Kun", "Pei", "Hu", "Ti", "Xu", "Hai", "Tang", "Lao", "Bu", "Jiao", "Xi", "Ju", "Li", "Xun", "Shi", "Cuo", 
            "Dun", "Qiong", "Xue", "Cu", "Bie", "Bo", "Ta", "Jian", "Fu", "Qiang", "Zhi", "Fu", "Shan", "Li", "Tuo", "Jia", 
            "Bo", "Tai", "Kui", "Qiao", "Bi", "Xian", "Xian", "Ji", "Jiao", "Liang", "Ji", "Chuo", "Huai", "Chi", "Zhi", "Dian", 
            "Bo", "Zhi", "Jian", "Die", "Chuai", "Zhong", "Ju", "Duo", "Cuo", "Pian", "Rou", "Nie", "Pan", "Qi", "Chu", "Jue", 
            "Pu", "Fan", "Cu", "Zhu", "Lin", "Chan", "Lie", "Zuan", "Xie", "Zhi", "Diao", "Mo", "Xiu", "Mo", "Pi", "Hu", 
            "Jue", "Shang", "Gu", "Zi", "Gong", "Su", "Zhi", "Zi", "Qing", "Liang", "Yu", "Li", "Wen", "Ting", "Ji", "Pei", 
            "Fei", "Sha", "Yin", "Ai", "Xian", "Mai", "Chen", "Ju", "Bao", "Tiao", "Zi", "Yin", "Yu", "Chuo", "Wo", "Mian", 
            "Yuan", "Tuo", "Zhui", "Sun", "Jun", "Ju", "Luo", "Qu", "Chou", "Qiong", "Luan", "Wu", "Zan", "Mou", "Ao", "Liu", 
            "Bei", "Xin", "You", "Fang", "Ba", "Ping", "Nian", "Lu", "Su", "Fu", "Hou", "Tai", "Gui", "Jie", "Wei", "Er", 
            "Ji", "Jiao", "Xiang", "Xun", "Geng", "Li", "Lian", "Jian", "Shi", "Tiao", "Gun", "Sha", "Huan", "Ji", "Qing", "Ling", 
            "Zou", "Fei", "Kun", "Chang", "Gu", "Ni", "Nian", "Diao", "Shi", "Zi", "Fen", "Die", "E", "Qiu", "Fu", "Huang", 
            "Bian", "Sao", "Ao", "Qi", "Ta", "Guan", "Yao", "Le", "Biao", "Xue", "Man", "Min", "Yong", "Gui", "Shan", "Zun", 
            "Li", "Da", "Yang", "Da", "Qiao", "Man", "Jian", "Ju", "Rou", "Gou", "Bei", "Jie", "Tou", "Ku", "Gu", "Di", 
            "Hou", "Ge", "Ke", "Bi", "Lou", "Qia", "Kuan", "Bin", "Du", "Mei", "Ba", "Yan", "Liang", "Xiao", "Wang", "Chi", 
            "Xiang", "Yan", "Tie", "Tao", "Yong", "Biao", "Kun", "Mao", "Ran", "Tiao", "Ji", "Zi", "Xiu", "Quan", "Jiu", "Bin", 
            "Huan", "Lie", "Me", "Hui", "Mi", "Ji", "Jun", "Zhu", "Mi", "Qi", "Ao", "She", "Lin", "Dai", "Chu", "You", 
            "Xia", "Yi", "Qu", "Du", "Li", "Qing", "Can", "An", "Fen", "You", "Wu", "Yan", "Xi", "Qiu", "Han", "Zha"
         };
        private static string[] pinYinArray = new string[] { 
            "a", "b", "c", "d", "e", "f", "g", "h", "j", "k", "l", "m", "n", "o", "p", "q", 
            "r", "s", "t", "w", "x", "y", "z"
         };
        private static string[] pyName = new string[] { 
            "A", "Ai", "An", "Ang", "Ao", "Ba", "Bai", "Ban", "Bang", "Bao", "Bei", "Ben", "Beng", "Bi", "Bian", "Biao", 
            "Bie", "Bin", "Bing", "Bo", "Bu", "Ba", "Cai", "Can", "Cang", "Cao", "Ce", "Ceng", "Cha", "Chai", "Chan", "Chang", 
            "Chao", "Che", "Chen", "Cheng", "Chi", "Chong", "Chou", "Chu", "Chuai", "Chuan", "Chuang", "Chui", "Chun", "Chuo", "Ci", "Cong", 
            "Cou", "Cu", "Cuan", "Cui", "Cun", "Cuo", "Da", "Dai", "Dan", "Dang", "Dao", "De", "Deng", "Di", "Dian", "Diao", 
            "Die", "Ding", "Diu", "Dong", "Dou", "Du", "Duan", "Dui", "Dun", "Duo", "E", "En", "Er", "Fa", "Fan", "Fang", 
            "Fei", "Fen", "Feng", "Fo", "Fou", "Fu", "Ga", "Gai", "Gan", "Gang", "Gao", "Ge", "Gei", "Gen", "Geng", "Gong", 
            "Gou", "Gu", "Gua", "Guai", "Guan", "Guang", "Gui", "Gun", "Guo", "Ha", "Hai", "Han", "Hang", "Hao", "He", "Hei", 
            "Hen", "Heng", "Hong", "Hou", "Hu", "Hua", "Huai", "Huan", "Huang", "Hui", "Hun", "Huo", "Ji", "Jia", "Jian", "Jiang", 
            "Jiao", "Jie", "Jin", "Jing", "Jiong", "Jiu", "Ju", "Juan", "Jue", "Jun", "Ka", "Kai", "Kan", "Kang", "Kao", "Ke", 
            "Ken", "Keng", "Kong", "Kou", "Ku", "Kua", "Kuai", "Kuan", "Kuang", "Kui", "Kun", "Kuo", "La", "Lai", "Lan", "Lang", 
            "Lao", "Le", "Lei", "Leng", "Li", "Lia", "Lian", "Liang", "Liao", "Lie", "Lin", "Ling", "Liu", "Long", "Lou", "Lu", 
            "Lv", "Luan", "Lue", "Lun", "Luo", "Ma", "Mai", "Man", "Mang", "Mao", "Me", "Mei", "Men", "Meng", "Mi", "Mian", 
            "Miao", "Mie", "Min", "Ming", "Miu", "Mo", "Mou", "Mu", "Na", "Nai", "Nan", "Nang", "Nao", "Ne", "Nei", "Nen", 
            "Neng", "Ni", "Nian", "Niang", "Niao", "Nie", "Nin", "Ning", "Niu", "Nong", "Nu", "Nv", "Nuan", "Nue", "Nuo", "O", 
            "Ou", "Pa", "Pai", "Pan", "Pang", "Pao", "Pei", "Pen", "Peng", "Pi", "Pian", "Piao", "Pie", "Pin", "Ping", "Po", 
            "Pu", "Qi", "Qia", "Qian", "Qiang", "Qiao", "Qie", "Qin", "Qing", "Qiong", "Qiu", "Qu", "Quan", "Que", "Qun", "Ran", 
            "Rang", "Rao", "Re", "Ren", "Reng", "Ri", "Rong", "Rou", "Ru", "Ruan", "Rui", "Run", "Ruo", "Sa", "Sai", "San", 
            "Sang", "Sao", "Se", "Sen", "Seng", "Sha", "Shai", "Shan", "Shang", "Shao", "She", "Shen", "Sheng", "Shi", "Shou", "Shu", 
            "Shua", "Shuai", "Shuan", "Shuang", "Shui", "Shun", "Shuo", "Si", "Song", "Sou", "Su", "Suan", "Sui", "Sun", "Suo", "Ta", 
            "Tai", "Tan", "Tang", "Tao", "Te", "Teng", "Ti", "Tian", "Tiao", "Tie", "Ting", "Tong", "Tou", "Tu", "Tuan", "Tui", 
            "Tun", "Tuo", "Wa", "Wai", "Wan", "Wang", "Wei", "Wen", "Weng", "Wo", "Wu", "Xi", "Xia", "Xian", "Xiang", "Xiao", 
            "Xie", "Xin", "Xing", "Xiong", "Xiu", "Xu", "Xuan", "Xue", "Xun", "Ya", "Yan", "Yang", "Yao", "Ye", "Yi", "Yin", 
            "Ying", "Yo", "Yong", "You", "Yu", "Yuan", "Yue", "Yun", "Za", "Zai", "Zan", "Zang", "Zao", "Ze", "Zei", "Zen", 
            "Zeng", "Zha", "Zhai", "Zhan", "Zhang", "Zhao", "Zhe", "Zhen", "Zheng", "Zhi", "Zhong", "Zhou", "Zhu", "Zhua", "Zhuai", "Zhuan", 
            "Zhuang", "Zhui", "Zhun", "Zhuo", "Zi", "Zong", "Zou", "Zu", "Zuan", "Zui", "Zun", "Zuo"
         };
        private static int[] pyValue = new int[] { 
            -20319, -20317, -20304, -20295, -20292, -20283, -20265, -20257, -20242, -20230, -20051, -20036, -20032, -20026, -20002, -19990, 
            -19986, -19982, -19976, -19805, -19784, -19775, -19774, -19763, -19756, -19751, -19746, -19741, -19739, -19728, -19725, -19715, 
            -19540, -19531, -19525, -19515, -19500, -19484, -19479, -19467, -19289, -19288, -19281, -19275, -19270, -19263, -19261, -19249, 
            -19243, -19242, -19238, -19235, -19227, -19224, -19218, -19212, -19038, -19023, -19018, -19006, -19003, -18996, -18977, -18961, 
            -18952, -18783, -18774, -18773, -18763, -18756, -18741, -18735, -18731, -18722, -18710, -18697, -18696, -18526, -18518, -18501, 
            -18490, -18478, -18463, -18448, -18447, -18446, -18239, -18237, -18231, -18220, -18211, -18201, -18184, -18183, -18181, -18012, 
            -17997, -17988, -17970, -17964, -17961, -17950, -17947, -17931, -17928, -17922, -17759, -17752, -17733, -17730, -17721, -17703, 
            -17701, -17697, -17692, -17683, -17676, -17496, -17487, -17482, -17468, -17454, -17433, -17427, -17417, -17202, -17185, -16983, 
            -16970, -16942, -16915, -16733, -16708, -16706, -16689, -16664, -16657, -16647, -16474, -16470, -16465, -16459, -16452, -16448, 
            -16433, -16429, -16427, -16423, -16419, -16412, -16407, -16403, -16401, -16393, -16220, -16216, -16212, -16205, -16202, -16187, 
            -16180, -16171, -16169, -16158, -16155, -15959, -15958, -15944, -15933, -15920, -15915, -15903, -15889, -15878, -15707, -15701, 
            -15681, -15667, -15661, -15659, -15652, -15640, -15631, -15625, -15454, -15448, -15436, -15435, -15419, -15416, -15408, -15394, 
            -15385, -15377, -15375, -15369, -15363, -15362, -15183, -15180, -15165, -15158, -15153, -15150, -15149, -15144, -15143, -15141, 
            -15140, -15139, -15128, -15121, -15119, -15117, -15110, -15109, -14941, -14937, -14933, -14930, -14929, -14928, -14926, -14922, 
            -14921, -14914, -14908, -14902, -14894, -14889, -14882, -14873, -14871, -14857, -14678, -14674, -14670, -14668, -14663, -14654, 
            -14645, -14630, -14594, -14429, -14407, -14399, -14384, -14379, -14368, -14355, -14353, -14345, -14170, -14159, -14151, -14149, 
            -14145, -14140, -14137, -14135, -14125, -14123, -14122, -14112, -14109, -14099, -14097, -14094, -14092, -14090, -14087, -14083, 
            -13917, -13914, -13910, -13907, -13906, -13905, -13896, -13894, -13878, -13870, -13859, -13847, -13831, -13658, -13611, -13601, 
            -13406, -13404, -13400, -13398, -13395, -13391, -13387, -13383, -13367, -13359, -13356, -13343, -13340, -13329, -13326, -13318, 
            -13147, -13138, -13120, -13107, -13096, -13095, -13091, -13076, -13068, -13063, -13060, -12888, -12875, -12871, -12860, -12858, 
            -12852, -12849, -12838, -12831, -12829, -12812, -12802, -12607, -12597, -12594, -12585, -12556, -12359, -12346, -12320, -12300, 
            -12120, -12099, -12089, -12074, -12067, -12058, -12039, -11867, -11861, -11847, -11831, -11798, -11781, -11604, -11589, -11536, 
            -11358, -11340, -11339, -11324, -11303, -11097, -11077, -11067, -11055, -11052, -11045, -11041, -11038, -11024, -11020, -11019, 
            -11018, -11014, -10838, -10832, -10815, -10800, -10790, -10780, -10764, -10587, -10544, -10533, -10519, -10331, -10329, -10328, 
            -10322, -10315, -10309, -10307, -10296, -10281, -10274, -10270, -10262, -10260, -10256, -10254
         };
        private static string[] referenceChinese = new string[] { 
            "吖", "八", "嚓", "哒", "屙", "发", "旮", "铪", "丌", "咔", "垃", "妈", "拿", "噢", "趴", "七", 
            "蚺", "仨", "他", "哇", "夕", "丫", "匝"
         };
        private static Regex regex = new Regex("[一-龥]$");
        public const string SqlKey = @"and |or |exec |execute |insert |select |delete |union |update |alter |create |drop |count |\* |chr |char |limit |asc |mid |'%|%'|substring |master |truncate |declare |xp_cmdshell |xp_ |sp_ |restore |backup |net +user |net +localgroup +administrators| and| or| exec| execute| insert| select| delete| union| update| alter| create| drop| count|\*|chr\(|char\(| limit| asc| mid| substring| master| truncate| declare| xp_cmdshell| xp_| sp_| restore| backup| net +user| net +localgroup +administrators";
        public const string SqlKeyLeft = @"and |or |exec |execute |insert |select |delete |union |update |alter |create |drop |count |\* |chr |char |limit |asc |mid |'%|%'|substring |master |truncate |declare |xp_cmdshell |xp_ |sp_ |restore |backup |net +user |net +localgroup +administrators";
        public const string SqlKeyRight = @" and| or| exec| execute| insert| select| delete| union| update| alter| create| drop| count|\*|chr\(|char\(| limit| asc| mid| substring| master| truncate| declare| xp_cmdshell| xp_| sp_| restore| backup| net +user| net +localgroup +administrators";

        public static string AddStartEndChar(this string source, string addChar = ",")
        {
            if (!string.IsNullOrWhiteSpace(source))
            {
                if (!source.StartsWith(addChar))
                {
                    source = addChar + source;
                }
                if (!source.EndsWith(addChar))
                {
                    source = source + addChar;
                }
            }
            return source;
        }

        public static bool CheckBadWords(this string str, string chkword)
        {
            if ((chkword != null) && (chkword != ""))
            {
                string str2 = chkword;
                string str3 = "";
                string[] strArray = str2.Split(new char[] { '|' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    str3 = strArray[i].ToString();
                    if (str.IndexOf(str3) >= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool CheckilterStr(this string str, string chkword)
        {
            if ((chkword != null) && (chkword != ""))
            {
                string[] strArray = chkword.Split(new char[] { '\x00a7' });
                int num = 0;
                for (int i = 0; i < strArray.Length; i++)
                {
                    num = 0;
                    string[] strArray2 = strArray[i].Split(new char[] { ',' });
                    for (int j = 0; j < strArray2.Length; j++)
                    {
                        if (str.IndexOf(strArray2[j].ToString()) >= 0)
                        {
                            num++;
                        }
                    }
                    if (num == strArray2.Length)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool CheckSqlStr(this string inputString, string sqlStr = "")
        {
            if (string.IsNullOrEmpty(sqlStr))
            {
                sqlStr = @"and |or |exec |execute |insert |select |delete |union |update |alter |create |drop |count |\* |chr |char |limit |asc |mid |'%|%'|substring |master |truncate |declare |xp_cmdshell |xp_ |sp_ |restore |backup |net +user |net +localgroup +administrators| and| or| exec| execute| insert| select| delete| union| update| alter| create| drop| count|\*|chr\(|char\(| limit| asc| mid| substring| master| truncate| declare| xp_cmdshell| xp_| sp_| restore| backup| net +user| net +localgroup +administrators";
            }
            try
            {
                inputString = inputString.Trim();
                if (!string.IsNullOrEmpty(inputString))
                {
                    Regex regex = new Regex(@"\b(" + sqlStr + @")\b", RegexOptions.IgnoreCase);
                    if (regex.IsMatch(inputString.ToLower()))
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string Cid15To18(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            string[] strArray = new string[] { 
                "7", "9", "10", "5", "8", "4", "2", "1", "6", "3", "7", "9", "10", "5", "8", "4", 
                "2"
             };
            string[] strArray2 = new string[] { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2" };
            int num = 0;
            if (source.Length == 15)
            {
                source = source.Substring(0, 6) + "19" + source.Substring(6, source.Length - 6);
                for (int i = 0; i < source.Length; i++)
                {
                    num += int.Parse(source.Substring(i, 1)) * int.Parse(strArray[i]);
                }
                source = source + strArray2[num % 11];
                return source;
            }
            return null;
        }

        public static string CleanHtmlTags(this string html, bool removePunctuation)
        {
            html = html.StripEntities(true);
            html = html.StripTags(true);
            if (removePunctuation)
            {
                html = html.StripPunctuation(true);
            }
            return html;
        }

        private static string CloakText(this string personalInfo)
        {
            if (personalInfo != null)
            {
                StringBuilder builder = new StringBuilder();
                int length = personalInfo.Length;
                for (int i = 0; i < length; i++)
                {
                    builder.Append((int)personalInfo[i]);
                    builder.Append(",");
                }
                builder.Remove(builder.Length - 1, 1);
                StringBuilder builder2 = new StringBuilder();
                builder2.Append("\n<script language=\"javascript\">\n");
                builder2.Append("<!-- \n");
                builder2.Append("   document.write(String.fromCharCode(");
                builder2.Append(builder.ToString());
                builder2.Append("))\n");
                builder2.Append("// -->\n");
                builder2.Append("</script>\n");
                return builder2.ToString();
            }
            return ObjectHelper.NullString;
        }

        public static string Concat(string splitchar, params object[] values)
        {
            string source = "";
            foreach (object obj2 in values)
            {
                source = source + obj2.ToString() + splitchar;
            }
            return source.TrimEnd(splitchar);
        }

        public static long ConventInt64HashCode(this string value)
        {
            long num = 0L;
            if (!string.IsNullOrEmpty(value))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(value);
                byte[] buffer2 = new SHA256CryptoServiceProvider().ComputeHash(bytes);
                long num2 = BitConverter.ToInt64(buffer2, 0);
                long num3 = BitConverter.ToInt64(buffer2, 8);
                long num4 = BitConverter.ToInt64(buffer2, 0x18);
                num = (num2 ^ num3) ^ num4;
            }
            return num;
        }

        public static List<T> ConvertArrayToList<T>(this T[] values)
        {
            List<T> list = new List<T>();
            foreach (T local in values)
            {
                list.Add(local);
            }
            return list;
        }

        public static List<Guid> ConvertArrayToListGuid(this string[] array)
        {
            List<Guid> list = new List<Guid>();
            for (int i = 0; i < array.Length; i++)
            {
                list.Add(new Guid(array[i]));
            }
            return list;
        }

        public static List<int> ConvertArrayToListInt(this string[] array)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < array.Length; i++)
            {
                list.Add(array[i].ConvertInt());
            }
            return list;
        }

        public static List<long> ConvertArrayToListInt64(this string[] array)
        {
            List<long> list = new List<long>();
            for (int i = 0; i < array.Length; i++)
            {
                list.Add(array[i].ConvertInt64());
            }
            return list;
        }

        public static List<string> ConvertArrayToListString(this string[] array)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < array.Length; i++)
            {
                list.Add(array[i]);
            }
            return list;
        }

        public static string ConvertChineseFirstSpells(this string hzString, bool IsUpper = true)
        {
            byte[] bytes = new byte[2];
            StringBuilder builder = new StringBuilder();
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            char[] chArray = hzString.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                if (regex.IsMatch(chArray[i].ToString()))
                {
                    bytes = Encoding.Default.GetBytes(chArray[i].ToString());
                    num2 = bytes[0];
                    num3 = bytes[1];
                    num = ((num2 * 0x100) + num3) - 0x10000;
                    if ((num > -2050) || (num < -20319))
                    {
                        builder.Append(chArray[i]);
                    }
                    else
                    {
                        int index;
                        string str = chArray[i].ToString();
                        if (Array.IndexOf<string>(referenceChinese, str) == -1)
                        {
                            string[] array = new string[] { 
                                "吖", "八", "嚓", "哒", "屙", "发", "旮", "铪", "丌", "咔", "垃", "妈", "拿", "噢", "趴", "七", 
                                "蚺", "仨", "他", "哇", "夕", "丫", "匝", ""
                             };
                            array[0x17] = str;
                            Array.Sort<string>(array);
                            index = Array.IndexOf<string>(array, str);
                            builder.Append(IsUpper ? pinYinArray[index - 1].ToUpper() : pinYinArray[index - 1]);
                        }
                        else
                        {
                            index = Array.IndexOf<string>(referenceChinese, str);
                            builder.Append(IsUpper ? pinYinArray[index].ToUpper() : pinYinArray[index]);
                        }
                    }
                }
                else
                {
                    builder.Append(chArray[i].ToString());
                }
            }
            return builder.ToString();
        }

        public static string ConvertChineseSpell(this string hzString, bool IsAddSplit, char splitChar = ' ')
        {
            byte[] bytes = new byte[2];
            StringBuilder builder = new StringBuilder();
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            char[] chArray = hzString.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                if (regex.IsMatch(chArray[i].ToString()))
                {
                    bytes = Encoding.Default.GetBytes(chArray[i].ToString());
                    num2 = bytes[0];
                    num3 = bytes[1];
                    num = ((num2 * 0x100) + num3) - 0x10000;
                    if ((num > -2050) || (num < -20319))
                    {
                        builder.Append(chArray[i]);
                    }
                    else if (num <= -10247)
                    {
                        for (int j = 11; j >= 0; j--)
                        {
                            int index = j * 0x21;
                            if (num >= pyValue[index])
                            {
                                for (int k = index + 0x20; k >= index; k--)
                                {
                                    if (pyValue[k] <= num)
                                    {
                                        if (IsAddSplit)
                                        {
                                            builder.Append(splitChar).Append(pyName[k]);
                                        }
                                        else
                                        {
                                            builder.Append(pyName[k]);
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        int num8 = Array.IndexOf<string>(otherChinese, chArray[i].ToString());
                        if (num8 != -1M)
                        {
                            if (IsAddSplit)
                            {
                                builder.Append(splitChar).Append(otherPinYin[num8]);
                            }
                            else
                            {
                                builder.Append(otherPinYin[num8]);
                            }
                        }
                    }
                }
                else
                {
                    builder.Append(chArray[i].ToString());
                }
            }
            return builder.ToString().TrimStart(new char[] { splitChar });
        }

        public static DateTime ConvertCurrentDateTime(this string value)
        {
            if (value.IsNull())
            {
                return DateTime.Now;
            }
            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static DateTime ConvertDateTime(this string value)
        {
            if (value.IsNull())
            {
                return Convert.ToDateTime("1900-01-01 00:00:00");
            }
            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
                return Convert.ToDateTime("1900-01-01 00:00:00");
            }
        }

        public static DateTime ConvertDateTime(this string dateValue, string dateFormat)
        {
            return DateTime.ParseExact(dateValue, dateFormat, CultureInfo.CurrentCulture);
        }

        public static decimal ConvertDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0M;
            }
            return decimal.Parse(value);
        }

        public static double ConvertDouble(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0.0;
            }
            return double.Parse(value);
        }

        public static float ConvertFloat(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0f;
            }
            return float.Parse(value);
        }

        public static string ConvertFloat2ToString(this double source)
        {
            return source.ToString("##,###.000");
        }

        public static string ConvertGBToUTF8(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            return Encoding.GetEncoding("GB2312").GetString(Encoding.UTF8.GetBytes(source));
        }

        public static Guid ConvertGuid(this string value)
        {
            return new Guid(value);
        }

        public static string ConvertGuidID(this string inputString)
        {
            string[] strArray = inputString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string str = "";
            foreach (string str2 in strArray)
            {
                str = str + "Guid'" + str2 + "',";
            }
            return str.TrimEnd(new char[] { ',' });
        }

        public static string ConvertHexToOct(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            return Convert.ToInt16(source, 0x10).ToString();
        }

        public static string ConvertHexToString(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            byte[] bytes = new byte[source.Length / 2];
            for (int i = 0; i < source.Length; i += 2)
            {
                string str = Convert.ToInt32(source.Substring(i, 2), 0x10).ToString();
                bytes[i / 2] = Convert.ToByte(source.Substring(i, 2), 0x10);
            }
            return Encoding.Default.GetString(bytes);
        }

        public static string ConvertIDString(this string inputString, char splitString)
        {
            string[] strArray = inputString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string str = "";
            foreach (string str2 in strArray)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, splitString, str2, splitString, "," });
            }
            return str.TrimEnd(new char[] { ',' });
        }

        public static int ConvertInt(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            return int.Parse(value);
        }

        public static long ConvertInt64(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0L;
            }
            return long.Parse(value);
        }

        public static List<Guid> ConvertListGuid(this string value)
        {
            if (value.IsNull())
            {
                return new List<Guid>();
            }
            return value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ConvertArrayToListGuid();
        }

        public static List<int> ConvertListInt(this string value)
        {
            if (value.IsNull())
            {
                return new List<int>();
            }
            return value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ConvertArrayToListInt();
        }

        public static List<long> ConvertListInt64(this string value)
        {
            if (value.IsNull())
            {
                return new List<long>();
            }
            return value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ConvertArrayToListInt64();
        }

        public static List<string> ConvertListString(this string value)
        {
            if (value.IsNull())
            {
                return new List<string>();
            }
            return value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ConvertArrayToListString();
        }

        public static T[] ConvertListToArray<T>(this IEnumerable<T> objList)
        {
            T[] localArray = new T[objList.Count<T>()];
            int index = 0;
            foreach (T local in objList)
            {
                localArray[index] = local;
                index++;
            }
            return localArray;
        }

        public static string ConvertListToString<T>(this IEnumerable<T> list)
        {
            string str = "";
            foreach (T local in list)
            {
                str = str + local.ToString() + ",";
            }
            return str.TrimEndComma();
        }

        public static long ConvertLong(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0L;
            }
            return long.Parse(value);
        }

        public static string ConvertO1(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            return decimal.Parse(source).ToString("#,##0");
        }

        public static string ConvertOctToHex(this int source)
        {
            return source.ToString("X2");
        }

        public static string ConvertStringID(this string inputString)
        {
            return inputString.ConvertIDString('\'');
        }

        public static string ConvertStringID<T>(this IEnumerable<T> list)
        {
            string str = "";
            foreach (T local in list)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "'", local, "'," });
            }
            return str.TrimEnd(new char[] { ',' });
        }

        public static string ConvertStringID<T>(this T[] list)
        {
            string str = "";
            foreach (T local in list)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "'", local, "'," });
            }
            return str.TrimEnd(new char[] { ',' });
        }

        public static int[] ConvertToArrayInt(this string value, char splitValue = ',')
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new int[0];
            }
            string[] strArray = value.Split(new char[] { splitValue }, StringSplitOptions.RemoveEmptyEntries);
            int[] numArray = new int[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                numArray[i] = Convert.ToInt32(strArray[i]);
            }
            return numArray;
        }

        public static string ConvertToHex(this string Word)
        {
            if (string.IsNullOrEmpty(Word))
            {
                return "";
            }
            int length = Word.Length;
            string str2 = "";
            byte[] bytes = new byte[2];
            for (int i = 0; i < length; i++)
            {
                int num2;
                string s = Word.Substring(i, 1);
                bytes = Encoding.Default.GetBytes(s);
                int num5 = bytes.Length;
                if (num5.ToString() == "1")
                {
                    num2 = Convert.ToInt32(bytes[0]);
                    str2 = str2 + Convert.ToString(num2, 0x10);
                }
                else
                {
                    num2 = Convert.ToInt32(bytes[0]);
                    int num3 = Convert.ToInt32(bytes[1]);
                    str2 = str2 + Convert.ToString(num2, 0x10) + Convert.ToString(num3, 0x10);
                }
            }
            return str2.ToUpper();
        }

        public static string ConvertToUnicode(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(source.Substring(i, 1));
                if (bytes.Length > 1)
                {
                    string str = Convert.ToString((short)bytes[1], 0x10);
                    string str2 = Convert.ToString((short)bytes[0], 0x10);
                    str = ((str.Length == 1) ? "0" : "") + str;
                    str2 = ((str2.Length == 1) ? "0" : "") + str2;
                    builder.Append("&#" + Convert.ToInt32(str + str2, 0x10) + ";");
                }
            }
            return builder.ToString();
        }

        public static string ConvertToUTF8(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < source.Length; i++)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(source.Substring(i, 1));
                if (bytes.Length > 1)
                {
                    string str = Convert.ToString((short)bytes[1], 0x10);
                    string str2 = Convert.ToString((short)bytes[0], 0x10);
                    builder.Append(@"\u" + (((str.Length == 1) ? "0" : "") + str) + (((str2.Length == 1) ? "0" : "") + str2));
                }
            }
            return builder.ToString();
        }

        public static string ConvertUTF8ToGB(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            return Encoding.UTF8.GetString(Encoding.GetEncoding("GB2312").GetBytes(source));
        }

        public static string CutText(this string textData, int Length)
        {
            return textData.CutText(Length, true);
        }

        public static string CutText(this string textData, int Length, CutTextTailTye cutTextTailTyeValue)
        {
            if (cutTextTailTyeValue == CutTextTailTye.AddTail)
            {
                return textData.CutText(Length, true);
            }
            if (cutTextTailTyeValue == CutTextTailTye.RemoveTail)
            {
                return textData.CutText(Length, true, "");
            }
            return "";
        }

        public static string CutText(this string textData, int Length, bool Flag)
        {
            return textData.CutText(Length, Flag, "...");
        }

        public static string CutText(this string textData, int Length, bool Flag, string AddString)
        {
            if (string.IsNullOrEmpty(textData))
            {
                return "";
            }
            if (Encoding.Default.GetByteCount(textData) > Length)
            {
                if (textData == null)
                {
                    return "";
                }
                int num = 0;
                int length = 0;
                if (!Flag)
                {
                    textData = Regex.Replace(textData, @"\<[^\<^\>]*\>", "");
                    textData = textData.Replace("&nbsp;", "");
                }
                foreach (char ch in textData)
                {
                    if (ch > '\x007f')
                    {
                        num += 2;
                    }
                    else
                    {
                        num++;
                    }
                    if (num > Length)
                    {
                        if (AddString.IsNoNull())
                        {
                            textData = textData.Substring(0, length) + AddString;
                            return textData;
                        }
                        textData = textData.Substring(0, length);
                        return textData;
                    }
                    length++;
                }
            }
            return textData;
        }

        public static string CutWord(this string textData, int Length)
        {
            return textData.CutWord(Length, true);
        }

        public static string CutWord(this string textData, int Length, CutTextTailTye cutTextTailTyeValue)
        {
            if (cutTextTailTyeValue == CutTextTailTye.AddTail)
            {
                return textData.CutWord(Length, true);
            }
            if (cutTextTailTyeValue == CutTextTailTye.RemoveTail)
            {
                return textData.CutWord(Length, true, "");
            }
            return "";
        }

        public static string CutWord(this string textData, int Length, bool Flag)
        {
            return textData.CutWord(Length, Flag, "...");
        }

        public static string CutWord(this string textData, int Length, bool Flag, string AddString)
        {
            if (string.IsNullOrEmpty(textData))
            {
                return "";
            }
            if (!Flag)
            {
                textData = Regex.Replace(textData, @"\<[^\<^\>]*\>", "");
                textData = textData.Replace("&nbsp;", "");
            }
            if (textData.Length > Length)
            {
                if (AddString.IsNoNull())
                {
                    textData = textData.Substring(0, Length) + AddString;
                }
                else
                {
                    textData = textData.Substring(0, Length);
                }
            }
            return textData;
        }

        public static string FilterHtml(this string html, string filter)
        {
            Regex regex;
            Match match;
            if (string.IsNullOrEmpty(html))
            {
                return "";
            }
            string input = html;
            switch (filter.ToUpper())
            {
                case "SCRIPT":
                    input = Regex.Replace(input, "</?script[^>]*>", string.Empty);
                    match = new Regex("</?script[^>]*>", RegexOptions.IgnoreCase).Match(input);
                    while (match.Success)
                    {
                        input = input.Replace(match.Groups[0].ToString(), string.Empty);
                        match = match.NextMatch();
                    }
                    regex = new Regex("(javascript|jscript|vbscript|vbs):", RegexOptions.IgnoreCase);
                    for (match = regex.Match(input); match.Success; match = match.NextMatch())
                    {
                        input = input.Replace(match.Groups[0].ToString(), match.Groups[1].ToString() + "：");
                    }
                    regex = new Regex("on(mouse|exit|error|click|key)", RegexOptions.IgnoreCase);
                    for (match = regex.Match(input); match.Success; match = match.NextMatch())
                    {
                        input = input.Replace(match.Groups[0].ToString(), "<I>on" + match.Groups[1].ToString() + "</I>");
                    }
                    regex = new Regex("&#", RegexOptions.IgnoreCase);
                    for (match = regex.Match(input); match.Success; match = match.NextMatch())
                    {
                        input = input.Replace(match.Groups[0].ToString(), "<I>&#</I>");
                    }
                    return input;

                case "TABLE":
                    return Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(input, "</?table[^>]*>", string.Empty), "</?tr[^>]*>", string.Empty), "</?th[^>]*>", string.Empty), "</?td[^>]*>", string.Empty);

                case "CLASS":
                    regex = new Regex("(<[^>]+) class=[^ |^>]*([^>]*>)", RegexOptions.IgnoreCase);
                    for (match = regex.Match(input); match.Success; match = match.NextMatch())
                    {
                        input = input.Replace(match.Groups[0].ToString(), match.Groups[0].ToString() + " " + match.Groups[1].ToString());
                    }
                    return input;

                case "STYLE":
                    regex = new Regex("(<[^>]+) style=\"[^\"]*\"([^>]*>)", RegexOptions.IgnoreCase);
                    for (match = regex.Match(input); match.Success; match = match.NextMatch())
                    {
                        input = input.Replace(match.Groups[0].ToString(), match.Groups[0].ToString() + " " + match.Groups[1].ToString());
                    }
                    return input;

                case "XML":
                    return Regex.Replace(input, @"<\?xml[^>]*>", string.Empty);

                case "NAMESPACE":
                    return Regex.Replace(input, @"<\/?[a-z]+:[^>]*>", string.Empty);

                case "FONT":
                    return Regex.Replace(input, "</?font[^>]*>", string.Empty);

                case "MARQUEE":
                    return Regex.Replace(input, "</?marquee[^>]*>", string.Empty);

                case "OBJECT":
                    return Regex.Replace(Regex.Replace(Regex.Replace(input, "</?object[^>]*>", string.Empty), "</?param[^>]*>", string.Empty), "</?embed[^>]*>", string.Empty);
            }
            return input;
        }

        public static string FilterKeyword(this string source, string Keyword, bool isRemove, string replaceString = "***")
        {
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                if (string.IsNullOrWhiteSpace(source))
                {
                    return source;
                }
                foreach (string str in Keyword.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] strArray = str.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                    if (strArray.Length == 1)
                    {
                        source = source.Replace(strArray[0], isRemove ? "" : replaceString);
                        continue;
                    }
                    bool flag = true;
                    foreach (string str2 in strArray)
                    {
                        if (!source.Contains(str2))
                        {
                            flag = false;
                        }
                        if (!flag)
                        {
                            break;
                        }
                    }
                    if (flag)
                    {
                        foreach (string str2 in strArray)
                        {
                            source = source.Replace(str2, isRemove ? "" : replaceString);
                        }
                    }
                }
            }
            return source;
        }

        public static string FilterSql(this string sqlString)
        {
            if (sqlString == null)
            {
                return "";
            }
            sqlString = sqlString.Replace("'", "''");
            sqlString = sqlString.Replace(";", "");
            sqlString = sqlString.Replace("(", "（");
            sqlString = sqlString.Replace(")", "）");
            sqlString = sqlString.Replace("0x", "0 x");
            sqlString = sqlString.FilterSqlKey();
            return sqlString;
        }

        public static string FilterSqlKey(this string sqlString)
        {
            if (sqlString == null)
            {
                return "";
            }
            sqlString = sqlString.Replace(@"and |or |exec |execute |insert |select |delete |union |update |alter |create |drop |count |\* |chr |char |limit |asc |mid |'%|%'|substring |master |truncate |declare |xp_cmdshell |xp_ |sp_ |restore |backup |net +user |net +localgroup +administrators| and| or| exec| execute| insert| select| delete| union| update| alter| create| drop| count|\*|chr\(|char\(| limit| asc| mid| substring| master| truncate| declare| xp_cmdshell| xp_| sp_| restore| backup| net +user| net +localgroup +administrators", "", RegexOptions.IgnoreCase);
            return sqlString;
        }

        public static string FilterStringArray(this string source, string str2)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            source = source.Replace(str2, "");
            if (source != "")
            {
                source = source.Replace(",,", ",");
                char ch = source[0];
                if (ch.ToString() == ",")
                {
                    source = source.Substring(1, source.Length - 1);
                }
                ch = source[source.Length - 1];
                if (ch.ToString() == ",")
                {
                    source = source.Substring(0, source.Length - 1);
                }
            }
            return source;
        }

        public static string FilterToNumber(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            source = Regex.Replace(source, "[^0-9]*", "", RegexOptions.IgnoreCase);
            return source;
        }

        public static string FiterTag(this string inputValue)
        {
            return inputValue.ProcessFiterTag(true, ',');
        }

        public static string Format(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static string FormatDisableScripting(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "";
            }
            RegexOptions options = RegexOptions.Singleline | RegexOptions.IgnoreCase;
            string replacement = " ";
            input = Regex.Replace(input, "<script[^>]*>.*?</script[^><]*>", replacement, options);
            input = Regex.Replace(input, "<input[^>]*>.*?</input[^><]*>", replacement, options);
            input = Regex.Replace(input, "<object[^>]*>.*?</object[^><]*>", replacement, options);
            input = Regex.Replace(input, "<embed[^>]*>.*?</embed[^><]*>", replacement, options);
            input = Regex.Replace(input, "<applet[^>]*>.*?</applet[^><]*>", replacement, options);
            input = Regex.Replace(input, "<form[^>]*>.*?</form[^><]*>", replacement, options);
            input = Regex.Replace(input, "<option[^>]*>.*?</option[^><]*>", replacement, options);
            input = Regex.Replace(input, "<select[^>]*>.*?</select[^><]*>", replacement, options);
            input = Regex.Replace(input, "<iframe[^>]*>.*?</iframe[^><]*>", replacement, options);
            input = Regex.Replace(input, "<ilayer[^>]*>.*?</ilayer[^><]*>", replacement, options);
            input = Regex.Replace(input, "<form[^>]*>", replacement, options);
            input = Regex.Replace(input, "</form[^><]*>", replacement, options);
            input = Regex.Replace(input, "javascript:", replacement, options);
            input = Regex.Replace(input, "vbscript:", replacement, options);
            return input;
        }

        public static string FormatEmail(this string email)
        {
            string personalInfo = null;
            if ((email != null) && (email.Trim().Length != 0))
            {
                if (email.IndexOf("@") != -1)
                {
                    personalInfo = "<a href=\"mailto:" + email + "\">" + email + "</a>";
                }
                else
                {
                    personalInfo = email;
                }
            }
            return personalInfo.CloakText();
        }

        public static string FormatFilter(this string filterInput, FilterFlag filterType)
        {
            if (filterInput.IsNull())
            {
                return string.Empty;
            }
            string sql = filterInput;
            if ((filterType & FilterFlag.NoSQL) == FilterFlag.NoSQL)
            {
                return sql.FormatRemoveSQL();
            }
            if ((filterType & FilterFlag.NoMarkup) == FilterFlag.NoMarkup)
            {
                if (IncludesMarkup(sql))
                {
                    sql = sql.EncodeHtml();
                }
            }
            else if ((filterType & FilterFlag.NoScripting) == FilterFlag.NoScripting)
            {
                sql = sql.FormatDisableScripting();
            }
            if ((filterType & FilterFlag.MultiLine) == FilterFlag.MultiLine)
            {
                sql = sql.FormatMultiLine();
            }
            return sql;
        }

        public static string FormatMessage(this object[] values)
        {
            StringBuilder builder = new StringBuilder();
            foreach (object obj2 in values)
            {
                builder.AppendLine(obj2.FormatMessage());
            }
            return builder.ToString();
        }

        public static string FormatMessage(this object value)
        {
            try
            {
                if (value == null)
                {
                    return "null";
                }
                if (value is string)
                {
                    return (string)value;
                }
                if (value is Exception)
                {
                    StringBuilder builder = new StringBuilder();
                    Exception innerException = (Exception)value;
                    builder.Append("事件信息：").Append("\r\n").AppendLine(innerException.Message);
                    builder.Append("\r\n").Append("堆栈跟踪：").Append("<br>").AppendLine(innerException.StackTrace);
                    while (innerException.InnerException != null)
                    {
                        builder.Append("\r\n").Append("内部事件信息：").Append("\r\n").AppendLine(innerException.InnerException.Message);
                        builder.Append("\r\n").Append("内部堆栈跟踪：").Append("\r\n").AppendLine(innerException.InnerException.StackTrace);
                        innerException = innerException.InnerException;
                    }
                    return builder.ToString();
                }
                return value.JsonJsSerialize();
            }
            catch (Exception exception2)
            {
                return ("消息转换出现异常:" + exception2.Message);
            }
        }

        public static string FormatMultiLine(this string input)
        {
            input = input.Replace("\r\n", "<br>");
            return input.Replace("\r", "<br>");
        }

        public static string FormatRemoveSQL(this string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                return "";
            }
            if (sql != null)
            {
                string[] strArray = ";,--,create,drop,select,insert,delete,update,union,sp_,xp_".Split(new char[] { ',' });
                foreach (string str in strArray)
                {
                    sql = Regex.Replace(sql, str, " ", RegexOptions.IgnoreCase);
                }
                sql = sql.Replace("'", "''");
            }
            return sql;
        }

        public static string FormatReverse(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            char[] array = source.ToCharArray();
            Array.Reverse(array);
            source = new string(array);
            return source;
        }

        public static string FormatText(this string html, bool retainSpace)
        {
            string str2;
            string pattern = @"\s*<\s*[bB][rR]\s*/\s*>\s*";
            if (retainSpace)
            {
                str2 = " \n";
            }
            else
            {
                str2 = "\n";
            }
            return Regex.Replace(html, pattern, str2);
        }

        public static string FormatWebsite(object website)
        {
            string str = null;
            if (website != null)
            {
                str = website.ToString().Trim();
                if (str.Length <= 0)
                {
                    return str;
                }
                if (str.IndexOf(".") > -1)
                {
                    str = "<a href=\"" + ((str.IndexOf("://") == -1) ? "" : "http://") + str + "\">" + str + "</a>";
                }
            }
            return str;
        }

        public static byte[] FromHexString(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return new byte[0];
            }
            char[] chArray = source.ToCharArray();
            byte[] buffer = new byte[chArray.Length / 2];
            for (int i = 0; i < (chArray.Length / 2); i++)
            {
                buffer[i] = Convert.ToByte(chArray[i * 2].ToString() + chArray[(i * 2) + 1].ToString(), 0x10);
            }
            return buffer;
        }

        public static int GetByteLength(this string source)
        {
            return Encoding.Default.GetByteCount(source);
        }

        public static string GetCidInfo(string cid)
        {
            if (string.IsNullOrEmpty(cid))
            {
                return "";
            }
            string[] strArray2 = new string[0x5c];
            strArray2[11] = "北京";
            strArray2[12] = "天津";
            strArray2[13] = "河北";
            strArray2[14] = "山西";
            strArray2[15] = "内蒙古";
            strArray2[0x15] = "辽宁";
            strArray2[0x16] = "吉林";
            strArray2[0x17] = "黑龙江";
            strArray2[0x1f] = "上海";
            strArray2[0x20] = "江苏";
            strArray2[0x21] = "浙江";
            strArray2[0x22] = "安微";
            strArray2[0x23] = "福建";
            strArray2[0x24] = "江西";
            strArray2[0x25] = "山东";
            strArray2[0x29] = "河南";
            strArray2[0x2a] = "湖北";
            strArray2[0x2b] = "湖南";
            strArray2[0x2c] = "广东";
            strArray2[0x2d] = "广西";
            strArray2[0x2e] = "海南";
            strArray2[50] = "重庆";
            strArray2[0x33] = "四川";
            strArray2[0x34] = "贵州";
            strArray2[0x35] = "云南";
            strArray2[0x36] = "西藏";
            strArray2[0x3d] = "陕西";
            strArray2[0x3e] = "甘肃";
            strArray2[0x3f] = "青海";
            strArray2[0x40] = "宁夏";
            strArray2[0x41] = "新疆";
            strArray2[0x47] = "台湾";
            strArray2[0x51] = "香港";
            strArray2[0x52] = "澳门";
            strArray2[0x5b] = "国外";
            string[] strArray = strArray2;
            double num = 0.0;
            Regex regex = new Regex(@"^\d{17}(\d|x)$");
            if (!regex.Match(cid).Success)
            {
                return "";
            }
            cid = cid.ToLower();
            cid = cid.Replace("x", "a");
            if (strArray[int.Parse(cid.Substring(0, 2))] == null)
            {
                return "非法地区";
            }
            try
            {
                DateTime.Parse(cid.Substring(6, 4) + "-" + cid.Substring(10, 2) + "-" + cid.Substring(12, 2));
            }
            catch
            {
                return "非法生日";
            }
            for (int i = 0x11; i >= 0; i--)
            {
                char ch = cid[0x11 - i];
                num += (Math.Pow(2.0, (double)i) % 11.0) * int.Parse(ch.ToString(), NumberStyles.HexNumber);
            }
            if (!((num % 11.0) == 1.0))
            {
                return "非法证号";
            }
            return (strArray[int.Parse(cid.Substring(0, 2))] + "," + cid.Substring(6, 4) + "-" + cid.Substring(10, 2) + "-" + cid.Substring(12, 2) + "," + (((int.Parse(cid.Substring(0x10, 1)) % 2) == 1) ? "男" : "女"));
        }

        public static string GetFirstString(this string strOriginal, string strSymbol)
        {
            int index = strOriginal.IndexOf(strSymbol);
            if (index != -1)
            {
                strOriginal = strOriginal.Substring(0, index);
            }
            return strOriginal;
        }

        public static string GetLastString(this string strOriginal, string strSymbol)
        {
            int startIndex = strOriginal.LastIndexOf(strSymbol) + strSymbol.Length;
            strOriginal = strOriginal.Substring(startIndex);
            return strOriginal;
        }

        public static string GetRandomSwitchString(this string RandomString)
        {
            return RandomString.GetRandomSwitchString(new char[] { ',' });
        }

        private static int GetStringCount(string input, string compare)
        {
            int index = input.IndexOf(compare);
            if (index != -1)
            {
                return (1 + GetStringCount(input.Substring(index + compare.Length), compare));
            }
            return 0;
        }

        public static int GetStringIncludeCount(this string strOriginal, string strSymbol)
        {
            return (strOriginal.Length - strOriginal.Replace(strSymbol, string.Empty).Length);
        }

        public static string GetTwoMiddleFirstStr(this string strOriginal, string strFirst, string strLast)
        {
            strOriginal = strOriginal.GetFirstString(strLast);
            strOriginal = strOriginal.GetLastString(strFirst);
            return strOriginal;
        }

        public static string GetTwoMiddleLastStr(this string strOriginal, string strFirst, string strLast)
        {
            strOriginal = strOriginal.GetLastString(strFirst);
            strOriginal = strOriginal.GetFirstString(strLast);
            return strOriginal;
        }

        public static string GetUrlParameter(this string url, string key)
        {
            if (!string.IsNullOrWhiteSpace(url) && !string.IsNullOrWhiteSpace(key))
            {
                try
                {
                    if (url.IndexOf('?') >= 0)
                    {
                        url = url.Substring(url.IndexOf('?'));
                    }
                    url = url.TrimStart(new char[] { '?', '&' });
                    url = "&" + url;
                    Match match = Regex.Match(url, "&" + key + "=([^&]*)", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        return match.Value.Substring(2 + key.Length);
                    }
                    return "";
                }
                catch (Exception)
                {
                }
            }
            return "";
        }

        public static string HtmlToJS(this string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return "";
            }
            html = html.Replace("\n", "");
            html = html.Replace("\r", "");
            html = html.Replace("\r\n", "");
            html = html.Replace(Environment.NewLine, "");
            html = html.Replace(@"\", @"\\");
            html = html.Replace("\"", "\\\"");
            html = "document.writeln(\"" + html + "\");";
            return html;
        }

        private static bool IncludesMarkup(string input)
        {
            RegexOptions options = RegexOptions.Singleline | RegexOptions.IgnoreCase;
            string pattern = "<[^<>]*>";
            return Regex.IsMatch(input, pattern, options);
        }

        public static bool IpIsLoopback(this string ip)
        {
            return (ip == "127.0.0.1");
        }

        public static long IpToInt(this string sip)
        {
            string[] strArray = sip.Split(".".ToCharArray());
            if (strArray.Length != 4)
            {
                return -1L;
            }
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            if (!IsInt32(strArray[0]))
            {
                return -1L;
            }
            num = Convert.ToInt32(strArray[0]);
            if (!IsInt32(strArray[1]))
            {
                return -1L;
            }
            num2 = Convert.ToInt32(strArray[1]);
            if (!IsInt32(strArray[2]))
            {
                return -1L;
            }
            num3 = Convert.ToInt32(strArray[2]);
            if (!IsInt32(strArray[3]))
            {
                return -1L;
            }
            num4 = Convert.ToInt32(strArray[3]);
            long num5 = num;
            num5 = (num5 * 0x100L) + num2;
            num5 = (num5 * 0x100L) + num3;
            return ((num5 * 0x100L) + num4);
        }

        public static string IpToString(this long ip)
        {
            return ip.IpToString(false);
        }

        public static string IpToString(this long ip, bool compact)
        {
            int num4 = (int)(ip % 0x100L);
            ip /= 0x100L;
            int num3 = (int)(ip % 0x100L);
            ip /= 0x100L;
            int num2 = (int)(ip % 0x100L);
            ip /= 0x100L;
            int num = (int)(ip % 0x100L);
            string str = num.ToString();
            string str2 = num2.ToString();
            string str3 = num3.ToString();
            string str4 = num4.ToString();
            if (!compact)
            {
                if (num < 10)
                {
                    str = "00" + str;
                }
                else if (num < 100)
                {
                    str = "0" + str;
                }
                if (num2 < 10)
                {
                    str2 = "00" + str2;
                }
                else if (num2 < 100)
                {
                    str2 = "0" + str2;
                }
                if (num3 < 10)
                {
                    str3 = "00" + str3;
                }
                else if (num3 < 100)
                {
                    str3 = "0" + str3;
                }
                if (num4 < 10)
                {
                    str4 = "00" + str4;
                }
                else if (num4 < 100)
                {
                    str4 = "0" + str4;
                }
            }
            return (str + "." + str2 + "." + str3 + "." + str4);
        }

        public static bool IsContainsKeyword(this string source, string Keyword)
        {
            bool flag = false;
            if (string.IsNullOrWhiteSpace(Keyword))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(source))
            {
                return false;
            }
            source = source.ToLower();
            foreach (string str in Keyword.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] strArray = str.ToLower().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length == 1)
                {
                    if (source.Contains(strArray[0]))
                    {
                        flag = true;
                    }
                }
                else
                {
                    bool flag2 = true;
                    foreach (string str2 in strArray)
                    {
                        if (!source.Contains(str2))
                        {
                            flag2 = false;
                        }
                        if (!flag2)
                        {
                            break;
                        }
                    }
                    if (flag2)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    return flag;
                }
            }
            return flag;
        }

        public static bool IsContainsKeyword(this string source, string Keyword, out string ContainsKey)
        {
            ContainsKey = "";
            bool flag = false;
            if (string.IsNullOrWhiteSpace(Keyword))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(source))
            {
                return false;
            }
            source = source.ToLower();
            foreach (string str in Keyword.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] strArray = str.ToLower().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length == 1)
                {
                    if (source.Contains(strArray[0]))
                    {
                        flag = true;
                        ContainsKey = ContainsKey + str + "|";
                    }
                    continue;
                }
                bool flag2 = true;
                foreach (string str2 in strArray)
                {
                    if (!source.Contains(str2))
                    {
                        flag2 = false;
                    }
                    if (!flag2)
                    {
                        break;
                    }
                }
                if (flag2)
                {
                    flag = true;
                    ContainsKey = ContainsKey + str + "|";
                }
            }
            ContainsKey = ContainsKey.Trim(new char[] { '|' });
            return flag;
        }

        public static bool IsFloat(string src)
        {
            try
            {
                Convert.ToSingle(src);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsInt32(string src)
        {
            src = src.Trim();
            try
            {
                Convert.ToInt32(src);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsInt64(string src)
        {
            src = src.Trim();
            try
            {
                Convert.ToInt64(src);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsInteger(string source)
        {
            if ((source == null) || (source == ""))
            {
                return false;
            }
            return Regex.IsMatch(source, @"^((\+)\d)?\d*$");
        }

        public static bool IsNoNull(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsNoNullOrWhiteSpace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNull(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string ProcessFiterTag(this string inputValue, bool isTrimSplit = true, char splitChar = ',')
        {
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                return "";
            }
            // inputValue = inputValue.Replace(0xff0c, splitChar);
            inputValue = inputValue.Replace(":", "");
            inputValue = inputValue.Replace(";", "");
            inputValue = inputValue.Replace("；", "");
            string str = "";
            foreach (string str2 in inputValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                str = str + str2.Trim() + splitChar.ToString();
            }
            if (isTrimSplit)
            {
                return str.Trim(new char[] { splitChar });
            }
            return (string.IsNullOrWhiteSpace(str) ? "" : (splitChar.ToString() + str));
        }

        public static string[] SplitString(this string source, string str)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(str))
            {
                return null;
            }
            return Regex.Split(source, str, RegexOptions.IgnoreCase);
        }

        public static ArrayList SplitStringByLength(this string str, int len)
        {
            int num2;
            ArrayList list = new ArrayList();
            int num = str.Length / len;
            if ((str.Length % len) != 0)
            {
                for (num2 = 0; num2 <= num; num2++)
                {
                    if ((str.Length - (num2 * len)) > len)
                    {
                        list.Add(str.Substring(num2 * len, len));
                    }
                    else
                    {
                        list.Add(str.Substring(num2 * len, str.Length % len));
                    }
                }
                return list;
            }
            for (num2 = 0; num2 < num; num2++)
            {
                list.Add(str.Substring(num2 * len, len));
            }
            return list;
        }

        public static string StripEntities(this string html, bool retainSpace)
        {
            string str;
            if (string.IsNullOrWhiteSpace(html))
            {
                return "";
            }
            if (retainSpace)
            {
                str = " ";
            }
            else
            {
                str = "";
            }
            return Regex.Replace(html, "&[^;]*;", str);
        }

        public static string StripNoWord(this string html, bool retainSpace)
        {
            string str;
            if (string.IsNullOrWhiteSpace(html))
            {
                return "";
            }
            if (retainSpace)
            {
                str = " ";
            }
            else
            {
                str = "";
            }
            if (html == null)
            {
                return html;
            }
            return Regex.Replace(html, @"\W*", str);
        }

        public static string StripPunctuation(this string html, bool retainSpace)
        {
            string str2;
            if (string.IsNullOrWhiteSpace(html))
            {
                return "";
            }
            string str = @"[~!#\$%\^&*\(\)-+=\{\[\}\]\|;:\x22'<,>\.\?\\\t\r\v\f\n]";
            Regex regex = new Regex(str + @"\s");
            Regex regex2 = new Regex(@"\s" + str);
            html = html + " ";
            if (retainSpace)
            {
                str2 = " ";
            }
            else
            {
                str2 = "";
            }
            while (regex2.IsMatch(html))
            {
                html = regex2.Replace(html, str2);
            }
            while (regex.IsMatch(html))
            {
                html = regex.Replace(html, str2);
            }
            return html;
        }

        public static string StripTags(this string html, bool retainSpace)
        {
            string str;
            if (string.IsNullOrWhiteSpace(html))
            {
                return "";
            }
            if (retainSpace)
            {
                str = " ";
            }
            else
            {
                str = "";
            }
            return Regex.Replace(html, "<[^>]*>", str);
        }

        public static string StripWhiteSpace(this string html, bool retainSpace)
        {
            string str;
            if (string.IsNullOrWhiteSpace(html))
            {
                return "";
            }
            if (retainSpace)
            {
                str = " ";
            }
            else
            {
                str = "";
            }
            return Regex.Replace(html, @"\s+", str);
        }

        public static string SubString(string source, int resultLength)
        {
            try
            {
                if (Encoding.Default.GetByteCount(source) > resultLength)
                {
                    if (source == null)
                    {
                        return "";
                    }
                    int num = 0;
                    int length = 0;
                    foreach (char ch in source)
                    {
                        if (ch > '\x007f')
                        {
                            num += 2;
                        }
                        else
                        {
                            num++;
                        }
                        if (num > resultLength)
                        {
                            source = source.Substring(0, length);
                            return source;
                        }
                        length++;
                    }
                }
            }
            catch (Exception exception)
            {
                throw new ArgumentException(exception.ToString());
            }
            return source;
        }

        public static string SubString(string src, int startIndex, int length)
        {
            if ((startIndex < 0) || (startIndex >= src.Length))
            {
                return "";
            }
            if ((startIndex + length) >= src.Length)
            {
                return src.Substring(startIndex, src.Length - startIndex);
            }
            return src.Substring(startIndex, length);
        }

        public static string Trim(this string value, char objChar)
        {
            return value.Trim(new char[] { objChar });
        }

        public static string TrimComma(this string value)
        {
            return value.Trim(new char[] { ',' });
        }

        public static string TrimEnd(this string value, char objChar)
        {
            return value.TrimEnd(new char[] { objChar });
        }

        public static string TrimEnd(this string source, string value)
        {
            if (!(!string.IsNullOrEmpty(source) && source.EndsWith(value)))
            {
                return source;
            }
            return source.Substring(0, source.Length - value.Length);
        }

        public static string TrimEndComma(this string value)
        {
            return value.TrimEnd(new char[] { ',' });
        }

        public static string TrimStart(this string value, char objChar)
        {
            return value.TrimStart(new char[] { objChar });
        }

        public static string TrimStart(this string source, string value)
        {
            if (!(!string.IsNullOrEmpty(source) && source.StartsWith(value)))
            {
                return source;
            }
            return source.Substring(value.Length);
        }

        public static string TrimStartComma(this string value)
        {
            return value.TrimStart(new char[] { ',' });
        }

        public static string UBB2HTML(string source)
        {
            if ((source == null) || (source.Length == 0))
            {
                return "";
            }
            source = source.Replace("&nbsp;", "");
            source = source.Replace("\n", "<br>");
            source = Regex.Replace(source, @"\[url=(?<x>[^\]]*)\](?<y>[^\]]*)\[/url\]", "<a href=$1 target=_blank>$2</a>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[url\](?<x>[^\]]*)\[/url\]", "<a href=$1 target=_blank>$1</a>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[email=(?<x>[^\]]*)\](?<y>[^\]]*)\[/email\]", "<a href=$1>$2</a>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[email\](?<x>[^\]]*)\[/email\]", "<a href=$1>$1</a>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[flash](?<x>[^\]]*)\[/flash]", "<OBJECT codeBase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=4,0,2,0 classid=clsid:D27CDB6E-AE6D-11cf-96B8-444553540000 width=500 height=400><PARAM NAME=movie VALUE=\"$1\"><PARAM NAME=quality VALUE=high><embed src=\"$1\" quality=high pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash' type='application/x-shockwave-flash' width=500 height=400>$1</embed></OBJECT>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[img](?<x>[^\]]*)\[/img]", "<IMG SRC=\"$1\" border=0>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[color=(?<x>[^\]]*)\](?<y>[^\]]*)\[/color\]", "<font color=$1>$2</font>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[face=(?<x>[^\]]*)\](?<y>[^\]]*)\[/face\]", "<font face=$1>$2</font>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[size=1\](?<x>[^\]]*)\[/size\]", "<font size=1>$1</font>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[size=2\](?<x>[^\]]*)\[/size\]", "<font size=2>$1</font>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[size=3\](?<x>[^\]]*)\[/size\]", "<font size=3>$1</font>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[size=4\](?<x>[^\]]*)\[/size\]", "<font size=4>$1</font>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[size=5\](?<x>[^\]]*)\[/size\]", "<font size=5>$1</font>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[size=6\](?<x>[^\]]*)\[/size\]", "<font size=6>$1</font>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[align=(?<x>[^\]]*)\](?<y>[^\]]*)\[/align\]", "<align=$1>$2</align>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[fly](?<x>[^\]]*)\[/fly]", "<marquee width=90% behavior=alternate scrollamount=3>$1</marquee>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[move](?<x>[^\]]*)\[/move]", "<marquee scrollamount=3>$1</marquee>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[glow=(?<x>[^\]]*),(?<y>[^\]]*),(?<z>[^\]]*)\](?<w>[^\]]*)\[/glow\]", "<table width=$1 style='filter:glow(color=$2, strength=$3)'>$4</table>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[shadow=(?<x>[^\]]*),(?<y>[^\]]*),(?<z>[^\]]*)\](?<w>[^\]]*)\[/shadow\]", "<table width=$1 style='filter:shadow(color=$2, strength=$3)'>$4</table>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[center\](?<x>[^\]]*)\[/center\]", "<center>$1</center>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[b\](?<x>[^\]]*)\[/b\]", "<b>$1</b>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[i\](?<x>[^\]]*)\[/i\]", "<i>$1</i>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[u\](?<x>[^\]]*)\[/u\]", "<u>$1</u>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[code\](?<x>[^\]]*)\[/code\]", "<pre id=code><font size=1 face='Verdana, Arial' id=code>$1</font id=code></pre id=code>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[list\](?<x>[^\]]*)\[/list\]", "<ul>$1</ul>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[list=1\](?<x>[^\]]*)\[/list\]", "<ol type=1>$1</ol id=1>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[list=a\](?<x>[^\]]*)\[/list\]", "<ol type=a>$1</ol id=a>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[\*\](?<x>[^\]]*)\[/\*\]", "<li>$1</li>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[quote](?<x>.*)\[/quote]", "<center>—— 以下是引用 ——<table border='1' width='80%' cellpadding='10' cellspacing='0' ><tr><td>$1</td></tr></table></center>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[QT=*([0-9]*),*([0-9]*)\](.[^\[]*)\[\/QT]", "<embed src=$3 width=$1 height=$2 autoplay=true loop=false controller=true playeveryframe=false cache=false scale=TOFIT bgcolor=#000000 kioskmode=false targetcache=false pluginspage=http://www.apple.com/quicktime/>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[MP=*([0-9]*),*([0-9]*)\](.[^\[]*)\[\/MP]", "<object align=middle classid=CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95 class=OBJECT id=MediaPlayer width=$1 height=$2 ><param name=ShowStatusBar value=-1><param name=Filename value=$3><embed type=application/x-oleobject codebase=http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701 flename=mp src=$3 width=$1 height=$2></embed></object>", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, @"\[RM=*([0-9]*),*([0-9]*)\](.[^\[]*)\[\/RM]", "<OBJECT classid=clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA class=OBJECT id=RAOCX width=$1 height=$2><PARAM NAME=SRC VALUE=$3><PARAM NAME=CONSOLE VALUE=Clip1><PARAM NAME=CONTROLS VALUE=imagewindow><PARAM NAME=AUTOSTART VALUE=true></OBJECT><br><OBJECT classid=CLSID:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA height=32 id=video2 width=$1><PARAM NAME=SRC VALUE=$3><PARAM NAME=AUTOSTART VALUE=-1><PARAM NAME=CONTROLS VALUE=controlpanel><PARAM NAME=CONSOLE VALUE=Clip1></OBJECT>", RegexOptions.IgnoreCase);
            return source;
        }
    }
}

