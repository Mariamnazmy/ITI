//import { movieID, url, imgPath, movieDetailsData } from "./Vars.js";
$(()=>{
let movieid=location.search.split("=")[1];
let movieUrl = `https://api.themoviedb.org/3/movie/${movieid}?api_key=9813ce01a72ca1bd2ae25f091898b1c7`;
$.get(movieUrl,function(data){
$(`<div class="container">
<img src="${"https://image.tmdb.org/t/p/w500/" + data.poster_path}" alt="Move" />
                        <div>
                        <h3 id="moveName">${data.title}</h3>
                    <p>${data.overview}</p>
                    </div>
</div>`).appendTo("#movieData")

});

});

