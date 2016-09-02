(function () {

    var ua = navigator.userAgent.toLowerCase();

    var is = (ua.match(/\b(chrome|opera|safari|msie|firefox)\b/) || ['', 'mozilla'])[1];

    var r = '(?:' + is + '|version)[\\/: ]([\\d.]+)';

    var v = (ua.match(new RegExp(r)) || [])[1];

    jQuery.browser.is = is;

    jQuery.browser.ver = v;

    jQuery.browser[is] = true;

})();

(function (jQuery) {


    this.version = '@1.3';

    this.layer = { 'width': 200, 'height': 100 };

    this.title = '信息提示';

    this.time = 4000;

    this.anims = { 'type': 'slide', 'speed': 600 };
    this.timer1 = null;
    var topHeight = document.documentElement.scrollTop + document.documentElement.clientHeight - this.layer.height - 2;
    var topHeight_ = document.documentElement.scrollTop + document.documentElement.clientHeight-33;
    var status =0;
    this.isMessageClose=false;
        this.resetinit=function(title, text)
    {
    $("#message #message_title").html(title);
     $("#message #message_content").html(text);
    };
    this.inits = function (title, text) {

        if ($("#message").is("div")) { return; }
        var topHeight = document.documentElement.scrollTop + document.documentElement.clientHeight - this.layer.height - 2;
        $(document.body).prepend('<div id="message" style="border:#b9c9ef 1px solid;z-index:10000;width:' + this.layer.width + 'px;position:absolute; display:none;background:#cfdef4; bottom:0; top:' + topHeight + 'px;right:0; overflow:hidden;">  <div  style="border:1px solid #fff;border-bottom:none;width:100%;height:30px;font-size:12px;overflow:hidden;color:#1f336b;"><span id="message_close" style="float:right;padding:5px 0 10px 0;width:16px;line-height:auto;color:red;font-size:12px;font-weight:bold;text-align:center;cursor:pointer;overflow:hidden;">×</span><div style="padding:5px 0 10px 5px;line-height:18px;text-align:left;overflow:hidden;" id="close"><span id="message_title">' + title + '</span><span  style="padding-left:4px;"><a href="#" id="contraction" style="color: #424242;  text-decoration:none;"></a></span></div>  <div style="clear:both;"></div></div> <div  style="padding-bottom:5px;border:1px solid #fff;border-top:none;width:100%;height:auto;font-size:12px;"><div id="message_content" style="background:#fff;margin:0 5px 1px 5px;border:#b9c9ef 1px solid;padding:0px 0px 0px 5px;font-size:12px;width:' + (this.layer.width - 17) + 'px;height:' + (this.layer.height - 40) + 'px;color:#1f336b;text-align:left;overflow-y:scroll;">' + text + '</div></div></div>');

        function Open(){  
         
          switch (this.anims.type) {
         
            case 'slide':$("#message").css("height",this.layer.height);$("#message").css("top",topHeight+"px");$("#message").css("display","none"); $("#message").slideDown(this.anims.speed); $("#contraction").html("[收缩]");break;

            case 'fade':$("#message").css("height",this.layer.height);$("#message").css("top",topHeight+"px");$("#message").css("display","none"); $("#message").fadeIn(this.anims.speed); $("#contraction").html("[收缩]");break;

            case 'show':$("#message").css("height",this.layer.height);$("#message").css("top",topHeight+"px");$("#message").css("display","none"); $("#message").show(this.anims.speed); $("#contraction").html("[收缩]");break;

            default:$("#message").css("height",this.layer.height);$("#message").css("top",topHeight+"px");$("#message").css("display","none"); $("#message").slideDown(this.anims.speed);$("#contraction").html("[收缩]"); break;

        }        
       } 
       
	$("#contraction").click(function(){
       if(status==1)
       {
	        Open();
            status=0;
        }
        else
        {
             $("#message").slideToggle("slow");
         setTimeout('$("#message").css("display","block");$("#message").css("height","30"); $("#message").css("top", "'+topHeight_+'px");        $("#contraction").html("[展开]");',1000);
        status=1;
        }
		
	});

        $("#message_close").click(function () {

            setTimeout(CloseMessage, 1);
            status =1;
        });
//        $("#message").hover(function () {
//            clearTimeout(timer1);
//            timer1 = null;
//        }, function () {

//        //timer1 = setTimeout('this.close()', time);
//        });

    };


   

    this.show = function (title, text, time) {

     if (title == 0 || !title) title = this.title;
        if ($("#message").is("div")) {this.resetinit(title,text); return; }
         else
         {
            this.inits(title, text);
         }

        if (time >= 0) this.time = time;

        switch (this.anims.type) {

            case 'slide': $("#message").slideDown(this.anims.speed); break;

            case 'fade': $("#message").fadeIn(this.anims.speed); break;

            case 'show': $("#message").show(this.anims.speed); break;

            default: $("#message").slideDown(this.anims.speed); break;

        }

        if ($.browser.is == 'chrome') {

            setTimeout(function () {

                $("#message").remove();

                this.inits(title, text);

                $("#message").css("display", "block");

            }, this.anims.speed - (this.anims.speed / 5));

        }

        //$("#message").slideDown('slow');
        //this.rmmessage(this.time);
       //setTimeout('this.close()', this.time);
       if(status==0)
       {
        setTimeout('$("#message").slideToggle("slow")',this.time);
      
      setTimeout('$("#message").css("display","block");$("#message").css("height","30"); $("#message").css("top", "'+topHeight_+'px");        $("#contraction").html("[展开]");',this.time+1000);
        status=1;
        }
    };

    this.lays = function (width, height) {

        if ($("#message").is("div")) { return; }

        if (width != 0 && width) this.layer.width = width;

        if (height != 0 && height) this.layer.height = height;

    }

    this.anim = function (type, speed) {

        if ($("#message").is("div")) { return; }

        if (type != 0 && type) this.anims.type = type;

        if (speed != 0 && speed) {

            switch (speed) {

                case 'slow': ; break;

                case 'fast': this.anims.speed = 200; break;

                case 'normal': this.anims.speed = 400; break;

                default:

                    this.anims.speed = speed;

            }

        }

    }

    this.rmmessage = function (time) {

        if (time > 0) {

            timer1 = setTimeout('this.close()', time);

            //setTimeout('$("#message").remove()', time+1000);

        }

    };
    function  CloseMessage()  {
        switch (this.anims.type) {
            case 'slide': $("#message").slideUp(this.anims.speed); break;
            case 'fade': $("#message").fadeOut(this.anims.speed); break;
            case 'show': $("#message").hide(this.anims.speed); break;
            default: $("#message").slideUp(this.anims.speed); break;
        };
        setTimeout('$("#message").remove();', this.anims.speed);
        this.original();
        this.isMessageClose=true;
          
        
    }

    this.original = function () {

        this.layer = { 'width': 200, 'height': 100 };

        this.title = '信息提示';

        this.time = 4000;

        this.anims = { 'type': 'slide', 'speed': 600 };

    };

    jQuery.messager = this;

    return jQuery;

})(jQuery);

$(window).scroll(function () {
    
    $("#message").css("top", topHeight + "px");
});


 