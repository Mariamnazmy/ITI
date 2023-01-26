window.addEventListener("load",function(){
let startbtn=this.document.querySelector(".strat");
let welcome=this.document.querySelector(".welcome");
let score=document.querySelectorAll(".datarow")[1];
let killed=document.querySelectorAll(".datarow")[3];
let birds=['/images/bird1.gif','/images/bird2.gif','/images/bird3.gif'];
let enddiv=this.document.querySelector(".endgame");
let endtext=this.document.querySelector(".endtext");
let name=window.location.search.split("=")[1].replace("+"," ");
let playagin=this.document.querySelector(".playagin");
let sum=0;
let shot=0;
let seconds=60;
let killsound =new sound("kill.mp3");
let music =new sound("music.mp3");


enddiv.classList.add("invisible"); 
// function take name from user
let changename=function(){
    
   
   let lastscore=document.querySelectorAll(".last")[1];
   let lastdate=document.querySelectorAll(".last")[2];
   let previoususer=JSON.parse(this.localStorage.getItem(name));
    if(previoususer!=null){
        lastscore.innerText=`${previoususer.score}`;  
        lastdate.innerText= `${previoususer.date}`;
    }
    else{
        lastscore.innerHTML=`0`;
        lastdate.innerHTML=`first time`;
    }
    let welcomename=document.querySelector("h1");
    welcomename.append(name); 
    let tdelm=document.querySelectorAll(".datarow")[0];
    tdelm.innerHTML=`${name}`;
  }

changename();
//start game 
startbtn.onclick=function(){
    console.log("hi");
    welcome.classList.add("hidden");
    let birdsinterval=setInterval(createBirds,1700);
    let bombinterval=setInterval(createbomb,6000);
    setTimeout(()=>{
        music.play();
    },1000); 
// img of div end game 
    let endimg=document.querySelector(".end");
//timer
    let timer=document.querySelectorAll(".datarow")[2];
    let id=setInterval(()=>{
    if(seconds>0){
        seconds --;
        timer.innerHTML=seconds;
    }
    else{
        clearInterval(id);
        clearInterval(birdsinterval);
        clearInterval(bombinterval);
        let userdata={
            score:sum,
            date: new Date().toLocaleString()
        }
        localStorage.setItem(name,JSON.stringify(userdata)); //name of user is the key
       setTimeout(()=>{
        music.stop();
       },5000); 
        setTimeout(()=>{ 
        if(sum>=50){ endtext.innerHTML=`your score is ${sum}. you won`;
        welcome.classList.add("invisible") ;
        endimg.src="/images/win.png";
        enddiv.classList.add("visible");
        }
        else{ endtext.innerHTML=`your score is ${sum}. you lost`; welcome.classList.add("invisible") ; enddiv.classList.add("visible"); } },5000);
      
    }
},1000);


};



// function generate birds
function createBirds(){
   
    let body=this.document.querySelector("body");
    let imgelemnt=document.createElement("img");
    let random=Math.floor(Math.random()*birds.length);
    imgelemnt.src=birds[random];
    if(random==0){imgelemnt.classList.add("bluebird");}
    else if(random==1){imgelemnt.classList.add("blackbird");}
    else if(random==2){imgelemnt.classList.add("whitebird");}
    body.appendChild(imgelemnt);
    imgelemnt.onclick=function(){ // if we clicked on any bird it will calculate the score and killed birds 
       
        killsound.play();
        
        shot+=1;
        killed.innerHTML=shot;
        if(imgelemnt.classList.contains("bluebird")){
            sum=sum-10;
            score.innerHTML=sum;
        }
        else if(imgelemnt.classList.contains("blackbird")){
            sum=sum+10;
            score.innerHTML=sum;
        }
        else if(imgelemnt.classList.contains("whitebird")){
            sum=sum+5;
            score.innerHTML=sum;
        }

   imgelemnt.src='/images/giphy.gif';
   console.log(imgelemnt.src);
   setTimeout(()=>{
    imgelemnt.remove();
    
   },1000);
   
   

    }
    let birdeleft=0;
    let birdetop=Math.floor(Math.random()*(window.innerHeight-parseInt(imgelemnt.clientHeight)));
    imgelemnt.style.top=birdetop+"px";
    ///function moveright of birds
    function moveright(){
        birdeleft+=5;
        imgelemnt.style.left=birdeleft+"px";
        imgelemnt.style.top=birdetop+"px";
        if(birdeleft>innerWidth-parseInt(imgelemnt.clientWidth)-16){
            imgelemnt.remove();
            clearInterval(id);
        }
    }
    let id=setInterval(moveright,30);
    
}
///create bombs
let createbomb=function(){
    let contanier=document.querySelector(".contanier");
    let bomb ='/images/bomb.png';
    let bombelm=document.createElement("img");
    bombelm.src=bomb;
    bombelm.setAttribute("class","bombb");
    contanier.append(bombelm);
    let down=0;
    let bombleft=Math.floor(Math.random()*(window.innerWidth-bombelm.clientWidth));
    bombelm.style.left=bombleft+"px";
    function movedown(){
        down+=5;
        bombelm.style.left=bombleft+"px";
        bombelm.style.top=down+"px";
        if(down>innerHeight-200){
            bombelm.remove();
            clearInterval(id);
        }
   
    }
    let id=setInterval(movedown,50);
    bombelm.addEventListener("click",function(){
       
        
        console.log("clicked");
    let bomb=document.querySelector(".bombb");
    let leftbomb=bomb.offsetLeft-100;
    let rightbomb= bomb.offsetLeft+ bomb.offsetWidth+100;
    let topbomb=bomb.offsetTop-100;
    let downbomb=bomb.offsetTop+bomb.offsetHeight+100;
  
    let birdsmall= document.querySelectorAll("img[src='/images/bird1.gif']"); //blue
    let birdbig=  document.querySelectorAll("img[src='/images/bird2.gif']"); //black
    let birdmid= document.querySelectorAll("img[src='/images/bird3.gif']"); //white
   birdsmall.forEach((small)=>{
   if(small.offsetLeft>leftbomb&&small.offsetLeft<rightbomb && small.offsetTop>topbomb&& small.offsetTop<downbomb){
       shot+=1;
        killed.innerHTML=shot; 
        sum=sum-10;
       score.innerHTML=sum;  
       console.log("blue");
       killsound.play();
       small.src="images/giphy.gif";
       setTimeout(()=>{
        small.remove();
       },1000); 
      
   }
   
     });
   birdbig.forEach((big)=>{
    if(big.offsetLeft>leftbomb&&big.offsetLeft<rightbomb && big.offsetTop>topbomb&& big.offsetTop<downbomb){
        shot+=1;
        killed.innerHTML=shot;
        sum=sum+10;
        score.innerHTML=sum;
        console.log("black");
        killsound.play();
        big.src="images/giphy.gif";
       setTimeout(()=>{
        big.remove();
       },1000); 
    }
   });
   
   birdmid.forEach((element)=>{
    if(element.offsetLeft>leftbomb&&element.offsetLeft<rightbomb && element.offsetTop>topbomb&& element.offsetTop<downbomb){
        shot+=1;
        killed.innerHTML=shot;
        sum=sum+5;
        score.innerHTML=sum;
        console.log("white");
        killsound.play();
        element.src="images/giphy.gif";
       setTimeout(()=>{
        element.remove();
       },1000); 
    }
   })
    });
}//endbomb
//playagian button
playagin.onclick=function(){
   
    playagin.parentElement.remove();
    window.location.reload(true);
    welcome.classList.add("invisible");
  
}

function sound(src) {
    this.sound = document.createElement("audio");
    this.sound.src = src;
    this.sound.setAttribute("preload", "auto");
    this.sound.setAttribute("controls", "none");
    this.sound.style.display = "none";
    document.body.appendChild(this.sound);
    this.play = function(){
        this.sound.play();
    }
    this.stop = function(){
        this.sound.pause();
    }    
}

});
