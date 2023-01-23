self.onmessage = function(){ // we can replace self with this keyword self means this worker 
    var sum = 0;
        for(var i=0;i<10000000000;i++){
            sum += i
        }
        self.postMessage(sum) // send the sum to the other file that create the worker 
}