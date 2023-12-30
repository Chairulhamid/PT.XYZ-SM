// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#table_id').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excel',
                title: 'Excelllll',
                className: ' text-light far fa-file-excel bg-secondary',
                text:' Excel',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5]
                }
            },
            {
                extend: 'pdf',
                className: ' text-light far fa-file-pdf bg-success',
                text: ' PDF',
                
                exportOptions: {
                    columns: [1, 2, 3, 4, 5]
                }
            },
            {
                extend: 'print',
                className: ' text-light fas fa-download bg-info',
                text: ' Print',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5]
                }
            },
            'colvis',

        ],
        columnDefs: [{
            targets: [0],
            visible: false
        }],
        'ajax':  {
            'url': "/Employees/GetAll",
            'order' : [[0, 'asc']],
            'dataSrc' : ''
        },
        'columns': [
            {
                data: 'no', name: 'id', render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {"data" : "nik"},
            {
                "data" : "",
                'render': function (data, type, row, meta) {
                    return row['firstName'] + " " + row['lastName'];
                }
            },
            {
                "data": "phone",
                render: function (data, type, row, meta) {
                    if (row['phone'].search(0) == 0) {
                        return row['phone'].replace('0', '+62');
                    } else {
                        return row['phone'];
                    }
                }
            },
            {
                data: "salary",
                render: function (data, type) {
                    var number = $.fn.dataTable.render.number(',', '', '', 'Rp. ').display(data);
                    return number;
                }
            },
            {
                "data": "gender",
                'render': function (data, type, row, meta) {
                    if (row['gender'] == 0) {
                        return "Male"
                    }
                    return  "Female"
                }
                },
            {
                "data": "",
                'render': function (data, type, row, meta) {
                    return `<td scope=" row">  <a class="btn btn-warning btn-sm text-light" data-url=""  onclick="getData('${row.nik}')" title="Detail"><i class="fa fa-info-circle"></i> </a></td>
                                    <td scope=" row">  <a class="btn btn-primary btn-sm text-light" data-url=""  onclick="getNIK('${row.nik}')"  title="Update"><i class="far fa-edit"></i></a></td>
                                  <td scope=" row"> <button type="button" class="btn btn-danger btn-sm text-light"  onclick="Delele('${row.nik}')" title="Delete"> <i class="fas fa-trash-alt"></i></button></td>`;
                }
            }
        ]
    });
    $(function () {
        $("form[name='nameModal']").validate({
            rules: {
                nik: {
                    required: true
                },
                firstName: {
                    required: true
                },
                lastName: {
                    required: true
                },
                phone: {
                    required: true,
                    minlength: 10,
                    maxlength: 13
                },
                birthDate: {
                    required: true
                },
                salary: {
                    required: true,
                    number: true
                },
                email: {
                    required: true,
                    email: true
                },
                password: {
                    required: true,
                    minlength: 8
                },
                gender: {
                    required: true
                },
                universiry_id: {
                    required: true
                },
                degree: {
                    required: true
                },
                gpa: {
                    required: true
                },
                role_id: {
                    required: true
                }
            },
            messages: {
                nik: {
                    required: "Please enter your NIK"
                },
                firstName: {
                    required: "Please enter your first name"
                },
                lastName: {
                    required: "Please enter your last name"
                },
                phone: {
                    required: "Please enter your phone number",
                    minlength: "Phone number should be at least 10 characters",
                    maxlength: "Phone number can't be longer than 13 characters"
                },
                birthDate: {
                    required: "Please enter your birthdate"
                },
                salary: {
                    required: "Please enter your salary"
                },
                email: {
                    required: "Please enter your email",
                    email: "The email should be in the format: abc@domain.tld"
                },
                gender: {
                    required: "Please choose your gender"
                },
                password: {
                    required: "Please enter your password",
                    minlength: "Password should be at least 8 characters",
                },
                degree: {
                    required: "Please choose your degree"
                },
                gpa: {
                    required: "Please enter your GPA"
                },
                universiry_id: {
                    required: "Please choose your university"
                },
                role_id: {
                    required: "Please choose your role"
                }
            }
        });
        $('#btnAdd').click(function (e) {
            e.preventDefault();
            if ($('#formValidation').valid() == true) {
                InsertData();
            }
        });
        $('#btnUpdate').click(function (e) {
            e.preventDefault();
            if ($('#formValidation').valid() == true) {
                Update();
            }
        });
    });
});

$.ajax({
    url: "/Employees/GetAll",
    success: function (result) {
        var optionUniv = `<option value="" >---Choose University---</option>`;
        $.each(result, function (key, val) {
            optionUniv += `
                            <option value="${val.id}">${val.name}</option>`;
        });
        $('#university_id').html(optionUniv);
    }
});

function InsertData() {
    var obj = new Object();
    obj.NIK = $('#nik').val();
    obj.FirstName = $('#firstName').val();
    obj.LastName = $('#lastName').val();
    obj.PhoneNumber = $('#phone').val();
    obj.Gender = $('#gender').val();
    obj.BirthDate = $('#birthDate').val();
    obj.Salary = $('#salary').val();
    obj.Email = $('#email').val();
    obj.Password = $('#password').val();
    obj.Degree = $('#degree').val();
    obj.Gpa = $('#gpa').val();
    obj.UniversityId = $('#university_id').val();
    obj.Role_Id = $('#role_id').val();
    console.log(obj);
    $.ajax({
        url: "/Employees/Register",
        'type': 'POST',
        'data': {entity : obj}, //objek kalian
        'dataType': 'json',
    }).done((result) => {
        if (result == 200) {
        swal({
            title: "Good job!",
            text: "Data Berhasil Ditambahkan!!",
            icon: "success",
            button: "Okey!",
        }).then(function () {
            window.location = "https://localhost:44306/home/DataTable";
        });
        } else if (result == 400) {
            swal({
                title: "Failed!",
                text: "Data Gagal Dimasukan, NIK,Phone,Email Sudah Terdaftar!!",
                icon: "error",
                button: "Close",
            });
        }
    }).fail((error) => {
        
        swal({
            title: "Failed!",
            text: "Data Gagal Dimasukan!!",
            icon: "error",
            button: "Close",
        });
    })
}

function Delele(nik) {
    console.log(nik);
    swal({
        title: "Are you sure?",
        text: "Hapus Data Ini!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: "/Employees/Delete/" + nik,
                    type: "Delete",
                  /*  contentType: "application/json;charset=UTF-8",
                    dataType: "json",*/
                    success: function (result) {
                        swal({
                            title: "Good job!",
                            text: "Data Berhasil Di Hapus!",
                            icon: "success",
                            button: "Oke!",
                        });
                        $('#table_id').DataTable().ajax.reload();
                    },
                    error: function (errormessage) {
                        alert(errormessage.responseText);
                        swal({
                            title: "Failed!",
                            text: "Data Gagal dihapus!!",
                            icon: "error",
                            button: "Close",
                        });
                    }
                });
            } else {
                swal("Data Gagal Dihapus!");
            }
        });
    }

function getData(nik) {
    $.ajax({
        url: "/Employees/GetNikEmployees/" + nik,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            console.log(result[0].nik)
            var tanggal = result[0].birthDate.substr(0, 10);
            $('#dnik').val(result[0].nik);
            $('#dfirstName').val(result[0].firstName);
            $('#dlastName').val(result[0].lastName);
            $('#dphone').val(result[0].phoneNumber);
            $('#dbirthDate').val(tanggal);
            $('#dsalary').val(result[0].salary);
            $('#demail').val(result[0].email);
            if (result[0].gender === "Male") {
                $('#dgender').val(0);
            } else {
                $('#dgender').val(1);
            };
            $('#dpassword').val(result[0].password);
            $('#ddegree').val(result[0].degree);
            $('#dgpa').val(result[0].gpa);
            $('#duniversiry_id').val(result[0].universityId);
            $('#drole_id').val(result[0].role_Id);
            $('#hid').hide();
            $('#modalDetail').modal('show');
        },
        error: function (errormessage) {
            /*alert(errormessage.responseText);*/
            swal({
                title: "FAILED",
                text: "Data tidak ditemukan!",
                icon: "error"
            }).then(function () {
                window.location = "https://localhost:44306/home/DataTable";
            });
        }
    });
    return false;
}

function getNIK(nik) {
    console.log(nik)
    $.ajax({
        url: "/Employees/GetNikEmployees/" + nik,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var tanggal = result[0].birthDate.substr(0, 10);
            $('#nik').val(result[0].nik);
            $('#firstName').val(result[0].firstName);
            $('#lastName').val(result[0].lastName);
            $('#phone').val(result[0].phoneNumber);
            $('#birthDate').val(tanggal);
            $('#salary').val(result[0].salary);
            $('#email').val(result[0].email);
            if (result[0].gender === "Male") {
                $('#gender').val(0);
            } else {
                $('#gender').val(1);
            };
            $('#password').val(result[0].password);
            $('#degree').val(result[0].degree);
            $('#gpa').val(result[0].gpa);
            $('#universiry_id').val(result[0].universityId);
            $('#role_id').val(result[0].role_Id);
            $('#modalData').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
            $('#hidePass').hide();
            $('#hideRow').hide();
            $('#nik').prop('disabled', true); ;

        },
        error: function (errormessage) {
            /*alert(errormessage.responseText);*/
            swal({
                title: "FAILED",
                text: "Data tidak ditemukan!",
                icon: "error"
            }).then(function () {
                window.location = "https://localhost:44306/home/DataTable";
            });
        }
    });
    return false;
}
function Update() {
    var nik = $('#nik').val();
    var obj = new Object();
    obj.NIK = $('#nik').val();
    obj.FirstName = $('#firstName').val();
    obj.LastName = $('#lastName').val();
    obj.Phone = $('#phone').val();
    obj.Gender = $('#gender').val();
    obj.BirthDate = $('#birthDate').val();
    obj.Salary = $('#salary').val();
    obj.Email = $('#email').val();
    console.log(obj);
    $.ajax({
        url: "/Employees/Put/" + nik,
        type: "PUT",
        data: {id : nik ,entity : obj}, 
        /*contentType: "application/json;charset=utf-8",
        dataType: "json",*/
        success: function (result) {
            $('#modalData').modal('hide');
            $('#nik').val("");
            $('#firstName').val("");
            $('#lastName').val("");
            $('#phone').val("");
            $('#birthDate').val("");
            $('#salary').val("");
            $('#email').val("");
            $('#gender').val("");
            swal({
                title: "Good job!",
                text: "Data Berhasil Diipdate!!",
                icon: "success",
                button: "Okey!",
            }).then(function () {
                window.location = "https://localhost:44306/home/DataTable";
            });
        },
        error: function (errormessage) {
            swal({
                title: "FAILED",
                text: "Data Gagal Dihapus!",
                icon: "error"
            }).then(function () {
                window.location = "https://localhost:44306/home/DataTable";
            });
        }
    });
}

$.ajax({
    url: "https://localhost:44309/API/Universities",
    success: function (result) {
        var optionUniv = `<option value="" >---Choose University---</option>`;
        $.each(result, function (key, val) {
            optionUniv += `
                            <option value="${val.id}">${val.name}</option>`;
        });
        $('#university_id').html(optionUniv);
    }
});

$.ajax({
    url: "https://localhost:44309/API/Roles",
    success: function (result) {
        var optionRole = `<option value="" >---Choose Role---</option>`;
        $.each(result, function (key, val) {
            optionRole += `
                            <option value="${val.role_Id}">${val.role_Name}</option>`;
        });
        $('#role_id').html(optionRole);
    }
});

/*AJax Untuk Charts Gender*/
$.ajax({
    url: "https://localhost:44309/API/Employees/Gender",
    success: function (result) {
        var series = [];
        var label = [];
        $.each(result.result, function (key, val) {
            series.push(val.value);
            if (val.gender === 0) {
            label.push("Male");
            } else {
                label.push("Female");
            }
        });
            var options = {
                chart: {
                    type: 'donut'
                },
                series: series,
                    labels: label
            }
            var chart = new ApexCharts(document.querySelector("#chart"), options);
            chart.render();   
    }
});
/*AJax Untuk Charts Degree*/
$.ajax({
    url: "https://localhost:44309/API/Employees/CountDegree",
    success: function (result) {
        var series = [];
        var label = [];
        $.each(result.result, function (key, val) {
            series.push(val.value);
            label.push(val.degree);

        });
        var options = {
            chart: {
                type: 'pie'
            },
            series: series,
            labels: label
        }
        var chart = new ApexCharts(document.querySelector("#lulusan"), options);
        chart.render();
    }
});

/*Ajax Untuk Role*/
$.ajax({
    url: "https://localhost:44309/API/Employees/CountRole",
    success: function (result) {
        var dataRole = [];
        var jenisRole = [];
        $.each(result.result, function (key, val) {
            dataRole.push(val.value);
            if (val.role_Id === 3) {
                jenisRole.push("Director");
            } else if (val.role_Id === 5) {
                jenisRole.push("Manager");
            }
            else {
                jenisRole.push("Employee");
            }
        });
        var options = {
            chart: {
                type: 'bar',
            },
         
            series: [{
             
                data: dataRole
            }],
            xaxis: {
                categories: jenisRole
            }
        }
        var chart = new ApexCharts(document.querySelector("#bar"), options);

        chart.render();
    }
});
$.ajax({
    url: "https://localhost:44309/API/Employees/CountSalary2",
    success: function (result) {
        var data = [];
        var categories = [];
        $.each(result.result, function (key, val) {
            categories.push(val.label);
            data.push(val.value);
        });
        var options = {
            chart: {
                /*height: 280,*/
                type: "bar",
                stacked: true
            },
            plotOptions: {
                bar: {
                    horizontal: true
                }
            },
            series: [
                {
                    name: "Total",
                    data: data
                }
            ],
            xaxis: {
                categories: categories
            }
        };

        var chart = new ApexCharts(document.querySelector("#area"), options);
        chart.render();
    }
});

