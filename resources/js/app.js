function createUserElement(user) {
    var tr = document.createElement('tr');
    var tdId = document.createElement('td');
    var tdLogin = document.createElement('td');
    tdId.textContent = user.id;
    tdLogin.textContent = user.login;
    tr.appendChild(tdId);
    tr.appendChild(tdLogin);
    return tr;
}

fetch('/getallusers')
    .then(response => response.json())
    .then(users => {
        var userElements = users.map(createUserElement);

        var table = document.createElement('table');
        table.classList.add('table', 'table-striped', 'table-hover');
        var thead = document.createElement('thead');
        var trHead = document.createElement('tr');
        var thId = document.createElement('th');
        var thLogin = document.createElement('th');
        thId.classList.add('id-header');
        thLogin.classList.add('login-header');
        trHead.appendChild(thId);
        trHead.appendChild(thLogin);
        thead.appendChild(trHead);
        var tbody = document.createElement('tbody');
        userElements.forEach(userElement => {
            tbody.appendChild(userElement);
        });
        table.appendChild(thead);
        table.appendChild(tbody);

        var usersDiv = document.getElementById('users');
        usersDiv.appendChild(table);
    });


    function getCookie(name, callback) {
        var cookieValue = "";
        if (document.cookie && document.cookie !== "") {
          var cookies = document.cookie.split(";");
          for (var i = 0; i < cookies.length; i++) {
            var cookie = cookies[i].trim();
            if (cookie.substring(0, name.length + 1) === (name + "=")) {
              cookieValue = cookie.substring(name.length + 1);
              break;
            }
          }
        }
        callback(decodeURIComponent(cookieValue));
        }
        
        getCookie("username", function(username) {
        console.log("Username from cookie:", username);
        document.getElementById("welcomeMessage").innerHTML = "Добро пожаловать, " + username + "!";
        });

        fetch('/getallposts', { method: 'POST' })
            .then(response => response.json())
            .then(posts => {
                const postsContainer = document.getElementById('posts');
                posts.forEach(post => {
                    const postElement = document.createElement('div');
                    postElement.className = 'card mb-3';
                    postElement.innerHTML = `
                        <div class="card-body">
                            <h3 class="card-title">${post.title}</h3>
                            <p class="card-text">${post.text}</p>
                            <p class="card-text"><small class="text-muted">Автор: ${post.authorLogin}</small></p>
                        </div>
                    `;
                    postsContainer.appendChild(postElement);
                });
            })
            .catch(error => console.error(error));
        