


$(()=>{
  let img="https://image.tmdb.org/t/p/w500/";
  let linkall= "https://api.themoviedb.org/3/discover/movie?sort_by=popularity.desc&api_key=9813ce01a72ca1bd2ae25f091898b1c7";
  $.get(linkall,function(data){
  //console.log(data.results);
  data.results.forEach(movie => {
    $(`<div class="movie">
    <a href="./DetailsPage.html?id=${movie.id}">
    <img src="${img + movie.poster_path}" alt="Move" />
        <h4 id="moveName">${movie.title}</h4>
    </a>
    </div>`).appendTo("#main");
    
  });
  });



});
 
   