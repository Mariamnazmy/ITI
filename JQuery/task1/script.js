/////// Code it yourself :)

btn =$("#btnAdd");
con= $(".tasksBox");
taskinput=$("#taskInput");

btn.click(function(){
    console.log("hello");
   mydiv=$("<div></div>");
   taskinput2=taskinput.val();
    mytext=$("<label/>");
    mytext.text(taskinput2)
   // mytext.text($("#taskInput").val());
    mytext.addClass("task");
    mydiv.append(mytext);
    //con.append(mydiv);


    
    
   
    mydone = $(" <button> Done </button>");
    mydone.addClass("done");
    mydiv.append(mydone);
    //con.append(mydone);
    mydone.click(function(){
        
        $(this).parent().addClass("TaskDone");
    });

    mydelete = $(" <button> Delete </button>");
    mydelete.addClass("delete");
    mydiv.append(mydelete);
    con.append(mydiv);
    mydelete.click(function(){
        $(this).parent().remove();
      
        
     

});


    

});
