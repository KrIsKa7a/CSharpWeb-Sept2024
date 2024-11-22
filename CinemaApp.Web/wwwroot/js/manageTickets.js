// TODO: Reimplement using jQuery to smooth the implementation
function openManageTicketsModal(cinemaId) {
    fetch(`https://localhost:7081/TicketApi/GetMoviesByCinema/${cinemaId}`, {
        method: 'GET',
        credentials: 'include'
    })
        .then(response => response.json())
        .then(movies => {
            renderMoviesInModal(movies);
            $('#manageTicketsModal').modal('show');
        })
        .catch(error => {
            console.error("Error loading movies:", error);
            alert("An error occurred while loading movies.");
        });
}

function renderMoviesInModal(viewModel) {
    let modalHtml = `
        <div id="manageTicketsModal" class="modal fade" tabindex="-1" aria-labelledby="manageTicketsModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Manage Tickets</h5>
                        <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>Title</th>
                                    <th>Genre</th>
                                    <th>Duration</th>
                                    <th>Available Tickets</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>`;

    viewModel.movies.forEach(movie => {
        modalHtml += `
            <tr>
                <td>${movie.title}</td>
                <td>${movie.genre}</td>
                <td>${movie.duration}</td>
                <td><input type="number" id="availableTickets-${movie.id}" value="${movie.availableTickets}" min="0" class="form-control" /></td>
                <td><button class="btn btn-primary" onclick="updateAvailableTickets('${movie.id}', '${viewModel.id}')">Update</button></td>
            </tr>`;
    });

    modalHtml += `
                            </tbody>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>`;

    document.getElementById("manageTicketsModalContainer").innerHTML = modalHtml;
}

function updateAvailableTickets(movieId, cinemaId) {
    const availableTickets = document.getElementById(`availableTickets-${movieId}`).value;

    fetch('https://localhost:7081/TicketApi/UpdateAvailableTickets', {
        method: 'POST',
        credentials: 'include',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            CinemaId: cinemaId,
            MovieId: movieId,
            AvailableTickets: parseInt(availableTickets, 10)
        })
    })
    .then(response => {
        if (!response.ok) throw new Error("Failed to update tickets.");
        alert("Ticket availability updated successfully.");
    })
    .catch(error => {
        console.error("Error:", error);
        alert("An error occurred while updating tickets.");
    });
}

function buyTicketsModal(cinemaId, movieId) {
    fetch(`https://localhost:7081/TicketApi/GetTicketsAvailability`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            CinemaId: cinemaId,
            MovieId: movieId
        })
    })
        .then(response => response.json())
        .then(viewModel => {
            $("#CinemaId").val(viewModel.cinemaId);
            $("#MovieId").val(viewModel.movieId);
            $("#Quantity").prop("min", "1");
            $("#Quantity").prop("max", `${viewModel.availableTickets}`);
            $("#AvailableTickets").val(viewModel.availableTickets);
            $('#buyTicketModal').modal('show');
        })
        .catch(error => {
            console.error("Error loading movies:", error);
            alert("An error occurred while loading movies.");
        });
}