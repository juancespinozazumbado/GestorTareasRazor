$(document).ready(function () {

    cargarTareas("");

});


/*
  new Tarea { Titulo = "Tarea 1", FechaFinalizacion =  DateTime.Now, UsuarioId = User.Identity.Name, IsTerminada = false},
*/


function cargarTareas(query) {

    $.ajax({
        url: '/Index?handler=Tareas',
        type: 'GET',
        data: { searchQuery: query },
        success: function (data) {
             
            console.log(data);
            renderTareas(data);

        },
        error: function (error) {
            console.log(error);
        }
    });
   
}

function renderTareas(data) {


    var completas = data.filter(task => task.isTerminada === true);
    var pendientes = data.filter(task => task.isTerminada === false);

    console.log(completas);
    console.log(pendientes);

    let completedTasksHtml = '';
    let pendingTasksHtml = '';

    var hoy = Date.now();

    var tareasVencidas = data.filter(task => {
        const fecha = new Date(task.fechaFinalizacion);
        return fecha < hoy;
    });

    var tareasContTiempo = data.filter(task => {
        const fecha = new Date(task.fechaFinalizacion);
        return fecha > hoy;

    });

    $("#infoTareasCompletas").text(`Tareas : ${data.length}`);
    $("#infoTareasPendientes").text(`Pendientes: ${pendientes.length}`);
    $("#infoTareasContTiempo").text(`Completas: ${completas.length}`);
    $("#infoTareasTareasVencidas").text(`Vencidas: ${tareasVencidas.length}`);
       

    data.forEach(task => {

        const hoy = new Date();
        const dueDate = new Date(task.fechaFinalizacion);
        let colorClass = "";
        let condicion = "";

        // Determine the color based on the date comparison
        if (dueDate < hoy) {
            // Task is delayed
            colorClass = "text-danger"; // Red
            condicion = "Vencida"
        } else if (dueDate.toDateString() === hoy.toDateString()) {
            // Task is due today
            colorClass = "text-warning";
            condicion = "Por vencer"
        } else {
            // Task is still ongoing
            colorClass = "text-primary";
            condicion = "En tiemnpo"
        }


        const taskCard = `
                     <div class="col-md-12 col-lg-4 mb-4 mb-lg-0">
                        <div class="card">
                          <div class="d-flex justify-content-between p-3">
                            <h5 class=" mb-0">${task.titulo}</h5>
                            <div
                              class="${task.isTerminada ? "bg-success" : "bg-dark"} rounded-circle d-flex align-items-center justify-content-center shadow-1-strong"
                              style="width: 35px; height: 35px;">
                              <p class="text-white mb-0 small"> <i class="${task.isTerminada ? "bi bi-check2-circle" : "bi bi-x-circle"}"></i></p>
                            </div>
                          </div>

                          <div class="card-body">
                            <div class="d-flex justify-content-between">
                             
                               <p class="text-dark mb-0">Fecha : ${new Date(task.fechaFinalizacion).toLocaleDateString('es-ES', { year: 'numeric', month: 'long', day: 'numeric' })}
</p>
                                <h5 class="${colorClass} ">${condicion === "Vencida" ? "<s>" + condicion + "</s>" : condicion}  </h5>
                            </div>

                            <div class="d-flex justify-content-between mb-4">
                              <h5 class="lead mb-0">${task.descripcion}</h5>
                           
                            </div>

                            <div class="d-flex justify-content-between mb-2">
                             
                               <p class="s"><a href="#!" data-bs-toggle="modal" data-bs-target="#editTaskModal" data-id="${task.id}" class=" btn-edit text-muted">Editar<i class="fbi bi-pencil-square"></i></a></p>
                              <div class="ms-auto text-warning">
                                  <div class="d-flex flex-row">
                                       <p class="large"><a href="#!" data-id="${task.id}"  class="btn-Marcar ${!task.isTerminada ? "text-success" : "bg-secundary"} flex-fill me-3" data-mdb-ripple-color="dark">
                                          ${task.isTerminada ? "Cancelar" : "Completar"} <i class="${!task.isTerminada ? "bi bi-check-circle-fill" : "bi bi-x-circle-fill"}"></i> </a></p>
                                      
                                     <p class="large"><a href="#!" data-id="${task.id}" asp-page-hanlder="Delete" class="btn-delete text-danger flex-fill ms-1">Eliminar<i class="bi bi-trash3-fill"></i></a></p>
                                    </div>
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                            `;
        if (task.isTerminada) {
            completedTasksHtml += taskCard;
        } else {
            pendingTasksHtml += taskCard;
        }
    });

    $("#titulo-Pendientes").hide();
    $("#titulo-Comnpletas").hide();
    $("#Tareas-completas").empty();
    $("#Tareas-pendientes").empty();
  

    if (pendientes.length > 0) {
        $("#titulo-Pendientes").show();
        $("#Tareas-pendientes").append(pendingTasksHtml);
    }
    if (completas.length > 0) {
        $("#titulo-Comnpletas").show();
        $("#Tareas-completas").append(completedTasksHtml);
    }

}


/**************** BUSQUEDA **************************/

$("#search-input").on("input", function () {
    const searchQuery = $(this).val();
    cargarTareas(searchQuery);
});


$(document).on('click', '.btn-delete', function (e) {
    e.preventDefault();
    var taskId = $(this).data('id');
    // var token = $('input[name="__RequestVerificationToken"]').val();
     var token = $('input:hidden[name="__RequestVerificationToken"]').val();
    // Send AJAX request to delete the task
    $.ajax({
        url: '/Index?handler=Eliminar',
        type: 'POST',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", token);
        },
        data: {
            id: taskId
        }, 
        success: function (data ) {
            // Remove the card from the DOM after successful deletion
            /*  $('div[data-task-id="' + taskId + '"]').remove();*/
            cargarTareas();
        },
        error: function (err) {
            console.error('Error deleting task:', err);
        }
    });
});




$(document).on('click', '.btn-Marcar', function (e) {
    e.preventDefault();
    var taskId = $(this).data('id');
    // var token = $('input[name="__RequestVerificationToken"]').val();
    var token = $('input:hidden[name="__RequestVerificationToken"]').val();
    // Send AJAX request to delete the task
    $.ajax({
        url: '/Index?handler=MarcarTarea',
        type: 'POST',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", token);
        },
        data: {
            id: taskId
        },
        success: function (data) {
            // Remove the card from the DOM after successful deletion
            /*  $('div[data-task-id="' + taskId + '"]').remove();*/
            cargarTareas("");
        },
        error: function (err) {
            console.error('Error deleting task:', err);
        }
    });
});


/*

 *************** Crear una nueva Tarea *********************

*/

$(document).on('submit', '#createTaskForm', function (e) {
    e.preventDefault();

    // Gather form data
    var formData = $(this).serialize();

    // Send AJAX request to create a new task
    $.ajax({
        url: '/Index?handler=CrearTarea',
        type: 'POST',
        data: formData,
        success: function (data) {
            if (data.success) {
                // Hide the modal
                $('#createTaskModal').modal('hide');

                // Clear the form
                $('#createTaskForm')[0].reset();

                // Reload tasks
                cargarTareas("");
            } else {
                console.error('Error creating task');
            }
        },
        error: function (err) {
            console.error('Error creating task:', err);
        }
    });
});



/*

 *************** Editar una Tarea *********************

*/
$(document).on('click', '.btn-edit', function () {
    var taskId = $(this).data('id');

    // Fetch the task data (this could be done via AJAX if needed)
    $.ajax({
        url: '/Index?handler=DetallesTarea',
        type: 'GET',
        data: { id: taskId },
        success: function (data) {
            // Populate modal fields
            $('#editarTareaId').val(data.id);
            $('#editarTareaTitulo').val(data.titulo);
            $('#editarTareaDescripcion').val(data.descripcion);
            $('#editarTareaFechaFinalizacion').val(data.fechaFinalizacion);
       
            // Show the modal
            $('#editTaskModal').modal('show');
        },
        error: function (err) {
            console.error('Error fetching task details:', err);
        }
    });
});



$(document).on('submit', '#editTaskForm', function (e) {
    e.preventDefault();

    var formData = $(this).serialize(); 
    $.ajax({
        url: '/Index?handler=EditarTarea',
        type: 'POST',
        data: formData,
        success: function (data) {
            if (data.success) {
                // Hide the modal
                $('#editTaskModal').modal('hide');

                // Reload tasks
                cargarTareas("");
            } else {
                console.error('Error updating task');
            }
        },
        error: function (err) {
            console.error('Error updating task:', err);
        }
    });
});




 