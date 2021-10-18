(function(n,i,r,u){n.event.special.swipe.horizontalDistanceThreshold=100,r.defaultSettings({stringsMap:{c1tabTocLabel:"toc.label",c1tabIndexLabel:"index.label",c1tabSearchLabel:"search.label",c1tabFavoriesLabel:"favorites.label",c1headerText:"pageHeaderText"}});var e,s=["toc","index","search","favorites"],f={toc:0,index:1,search:2,favorites:3,0:0,1:1,2:2,3:3,c1tabToc:0,c1tabIndex:1,c1tabSearch:2,c1tabFavorites:3},o={};r.driver({name:"theme",create:function(){var i=n("#c1sideTabsHeader");n.each(["toc","index","favorites"],function(n,u){t=r.setting(u+".icons.tab"),t&&(n=f[u],i.children().eq(u=="favorites"?n-1:n).find("a").attr("data-icon",t))})}}),r.bind("writebody",function(){var c=i.designer,o=r.isTopicOnlyMode(),h=function(n,t,f){var e=n;if(t==u)return e.is(":visible")?e.attr("src"):!1;t?(i.isString(t)&&e.attr("src",t),f&&(e.css("cursor","pointer"),e.click(function(n){return f.call(r,n,this)})),e.show()):e.hide()},t,s,f;e=n("#c1page").parent(),e.length||(e=n("body")),o&&e.addClass("topic-only"),r.setting("general.rightToLeft")&&e.attr("dir","rtl").css("direction","rtl"),t=r.header={_header:n("#c1header"),_main:n("#c1contentInner"),_headerText:n("#c1headerText"),_barRight:n("#c1topBar"),_barLeft:n("#c1topLeftBar"),visible:function(n){var r=t._header;if(n==u)return r.is(":visible");n?o||(r.show(),t._main.css("top",r.height())):(r.hide(),t._main.css("top",0))},height:function(n){var i=t._header,e;if(n==u)return t.visible()?i.height():0;if(n=+n,!n||n<0)t.visible(!1);else if(!o){i.height(n).show(),t._main.css("top",i.height()),e=50;function f(){var u=t._headerText.height(),r=t._barRight.height(),i=t._barLeft.height();if(u+r+i===0){e--&&setTimeout(f,100);return}u&&t._headerText.css("top",(n-u)/2),r&&t._barRight.css("margin-top",(n-r)/2-1),i&&t._barLeft.css("margin-top",(n-i)/2-1)}r.ready(function(){f()})}},logo:function(n,f){var e=t._header.find(".c1-header-logo");if(n==u)return e.is(":visible")?e.attr("src"):!1;n?(i.isString(n)&&e.attr("src",n),f&&(e.css("cursor","pointer"),e.click(function(n){return f.call(r,n,this)})),e.show()):e.hide()},text:function(n){var r=t._header.find(".c1-header-text");if(n==u)return r.text();n?(i.isString(n)&&r.text(n),r.show()):r.hide()},html:function(n){if(n==u)return t._header.html();t._header.html(n)}},r.setSwatch(t._header,"pageHeader"),f=i.str(r.setting("sidePanel.header.logoImage"),""),s=i.getFunction(r.setting("sidePanel.header.logoClick")),h(n("#c1sidePanelLogo"),f,s),f=r.setting("pageHeader.visible"),f?(t.height(r.setting("pageHeader.height")),r.setting("pageHeader.showText",{types:"boolean"})===!1&&n("#c1headerText").hide()):t.visible(!1)}),r.plugin({name:"theme",create:function(){var ut=r.theme=r.theme||{},ot,st=i.designer,nt,b,g,d,l;if(r.setting("topic.applyStylesheet")!==!1&&n("#c1topicPanel").addClass("ui-widget-content"),r.isTopicOnlyMode())setTimeout(function(){e.addClass("topic-only")},100);else{var c=n("#c1sideTabs"),h=n("#c1sideTabsHeader"),rt=n("#c1sideTabsPanel"),it=!!r.setting("theme.rememberActiveTab"),tt="c1sideActiveTab",t=it&&n.cookie&&n.cookie(tt)||f[((/(?:\?|&|^)tab=([^&]+)(?:&|$)/i.exec(location.search)||[])[1]||"0").toLowerCase()],ft=r.index,p=3,v=h.children("li");n.each(["index","favorites"],function(n,t){if(i=r.setting(t+".visible"),!n&&i&&(i=ft.hasKeywords()||!r.setting("index.hideEmpty")),!i){n=f[t],o[n]=!0;var i=h.children().eq(t=="index"?n:n-1);v=v.not(i),i.add(rt.children().eq(n)).hide(),p--}}),h.removeClass("ui-grid-b"),h.addClass("ui-grid-"+(p>1?String.fromCharCode(97+p-2):"solo")),h.children("li").removeClass("ui-block-a ui-block-b ui-block-c ui-block-d"),v.each(function(t){n(this).addClass("ui-block-"+String.fromCharCode(97+t))}),r.setting("search.visible")===!1&&n("#c1side").addClass("nosearch");function et(t){var u=r.setting("sidePanel.header"),i=u.visible&&t!==!1?u.height||n("#c1sidePanelHeader").height():0,f=r.setting("search.visible")===!0?42:0;n("#c1sideSearch").css("top",i),n("#c1sideTabs").css("top",i+f)}nt=function(){var n=r.search,t=n.filterElement.val();r.switchTab("search")};n(document).on("keydown","#c1searchFilter",function(t){t.which===13&&n(this).val()&&nt()});b=ut.recalcTabsTop=function(){},c.show();function y(i){var u=n("#c1sidePanelHeader>img"),r=u.attr("src"),f=n("#c1sidePanelHeader"),e=!t&&!i||r?!0:!1;t||i?(n("#c1tocBack, #c1sidePanelHeaderTitle").hide(),r&&u.show(),f[r?"show":"hide"]()):(r&&u.hide(),n("#c1tocBack, #c1sidePanelHeaderTitle").show(),f.show()),et(e)}r.ready(function(){y(!0)}),r.bind("tocnavigate",function(n,t){y(t.root)}),c.bind("tabsshow",function(t,i){it&&n.cookie&&n.cookie(tt,i.index||null,{expires:365});var u=n("input",i.panel);u.length||(u=n("a",i.panel).first()),u.focus(),r.trigger("tab",t,{index:i.index,tab:s[i.index],panel:i.panel,label:i.tab}),y(i.index||!n("#c1sidePanelHeaderTitle").text())});function a(n){var f=rt.children().hide().eq(n).show(),r=h.children().find('[data-role="button"]').removeClass("ui-btn-active").end(),i=n==2?u:r.eq(n<3?n:n-1).find('[data-role="button"]').addClass("ui-btn-active")[0];t!=n&&(t=n,c.trigger("tabsshow",{index:n,panel:f[0],tab:i}))}o[t]||a(+t),r.activeTab=function(){return s[t]},r.switchTab=function(n){n=f[n],n!=u&&a(+n)},n("#c1sideTabsHeader").find('[data-role="button"]').click(function(){var t=n(this);return a(f[t.attr("href").substring(1)]),!1});function w(){b()}c.is(":visible")?w():(g=100,d=setInterval(function(){(c.is(":visible")||!g--)&&(clearInterval(d),w())},100));function k(){var t=n("#c1topic .topic-frame");t.length&&t.css("top",n("#c1topicBar").outerHeight()+2)}n(window).on("resize orientationchange",k);if(r.bind("topicupdate breadcrumbsupdate",k),l=function(){var u=n("#c1headerText"),i,t;u.data("initialized")&&(i=r.setting("general.rightToLeft")===!0,t={},t[i?"right":"left"]=n("#c1topLeftBar").width()+"px",t[i?"left":"right"]=n("#c1topBar").width(),t["text-overflow"]="clip",u.css(t))},r.bind("toolbarcollapse",l),l(),r.ready(function(){n("#c1side").show()}),n.mobile.support.touch)n("#c1contentInner").on("swipeleft",function(n){r.getPrev()&&r.gotoNext(n)}).on("swiperight",function(n){r.getNext()&&r.gotoPrev(n)})}}}),r.driver("themedesigner",function(){var t=i.designer;t&&(t.hooks["topic.spinner"]=function(t,i){r.topicSpin(i),n(".preview-popup").toggle(!i)},r.bind("tab",function(n,i){t.isDesigntime()&&t.select(i.panel)}),n.each(s,function(n,i){t.hooks[i]=function(n,t){t&&!o[f[i]]&&r.switchTab(i)}}))})})(jQuery,nethelp,nethelpshell)