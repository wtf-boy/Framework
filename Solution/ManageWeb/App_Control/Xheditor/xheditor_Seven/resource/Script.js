
//只允许输入数字
function IsDigit()
{
    return ((event.keyCode >= 48) && (event.keyCode <= 57));
}
//设置地址
function setUrl(txtUrl,e,url)
{
	var allChilds = document.getElementById("list").childNodes;
	$("#" + txtUrl).val(url);
	$("#list li").removeClass("selected");
	$(e).parent("li").addClass("selected");
}


//设置地址
function setFirstUrl()
{
	var allChilds = document.getElementById("list").childNodes;
    if(allChilds.length>0)
    {
	    allChilds[0].className = "selected";
    }    
}