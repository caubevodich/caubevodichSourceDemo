/**
FileName: ColorData.src.js
Project Name: ColorLife
Date Created: 29/6/2014 9:45:51 PM
Description: Auto-generated
Version: 1.0.0.0
Author:	Lê Thanh Tuấn - Khoa CNTT
Author Email: tuanitpro@gmail.com
Author Mobile: 0976060432
Author URI: http://tuanitpro.com
License: 
*/


(function ($) {
    $.extend({
        ColorLifeData: {
            IMGLoading: '<img src=\"/Content/Images/ajax-loader.gif\" border=\"0\" />',
            templateUrl: '/Content/templates/',
            imagesFolder: '/Content/images/',
            siteUrl: 'http://localhost',
            serviceUrl: '/api/',
            loader: function (isFade) {
                if (isFade) {

                    $('#Loader').fadeIn();
                } else {

                    $('#Loader').fadeOut();
                }
            },
            notifyInfo: function (msg) {
                toastr.info(msg, 'Thông báo', options = { "closeButton": true, "positionClass": "toast-top-right" });
            },
            notifySuccess: function (msg) {
                toastr.success(msg, 'Thành công', options = { "closeButton": true, "positionClass": "toast-top-right" });
            },
            notifyError: function (msg) {
                toastr.warning(msg, 'Lỗi', options = { "closeButton": true, "positionClass": "toast-top-right" });
            },
            // Validate Email
            validateEmail: function (email) {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                return reg.test(email);
            },
            validatePassword: function (password) {
                var reg = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&*]).{6,20}.*$/;
                return reg.test(password);
                // return true;
            },
            validatePasswordConfirm: function (password1, password2) {
                if (password2 != password1)
                    return false;
                return true;
            },
            jsonAccountChangePassword: function (form) {
                var obj = $(form);
                var ChangePasswordModel = {
                    Email: obj.find("#Email").val(),
                    PasswordOld: obj.find("#PasswordOld").val(),
                    PasswordNew: obj.find("#PasswordNew").val(),
                    PasswordNew2: obj.find("#PasswordNew2").val(),
                };
                if (ChangePasswordModel.PasswordOld.length == 0) {
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                    return false;
                }
                if (!$.ColorLifeData.validatePasswordConfirm(ChangePasswordModel.PasswordNew2, ChangePasswordModel.PasswordNew)) {
                    $.ColorLifeData.notifyError('Mật khẩu không khớp.');
                    return false;
                }
                if (ChangePasswordModel.PasswordOld.length && ChangePasswordModel.PasswordNew.length > 3 && ChangePasswordModel.PasswordNew2.length > 3) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "Account/ChangePassword/x",
                        data: JSON.stringify(ChangePasswordModel),
                        type: "PUT",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                        },
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonAccountChangePassword2: function (email, password, passwordNew) {
                var dto = { "email": email, "password": password, "passwordNew": passwordNew };
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "JsonUsers_ChangePassword",
                    data: JSON.stringify(dto),
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $(".span-update").html("<i class='icon-spinner icon-spin'></i>Updating...</span>");
                    },
                    success: function (result) {
                        if (result.Success) {
                            $("#changepassok").show();
                        }
                        else {
                            $("#changepasserror").show();
                        }
                    },
                    error: function (errormessage) {
                        //Hiển thị lỗi nếu xảy ra
                        alert(errormessage.responseText);
                        //alert("ERROR 500");
                    }
                });
                return false;
            },

            deleteConfirm: function (message, callback) {
                new BootstrapDialog({
                    title: 'Xác nhận',
                    // type: BootstrapDialog.TYPE_WARNING,
                    message: message,
                    closable: false,
                    draggable: true,
                    data: {
                        'callback': callback
                    },
                    buttons: [{
                        label: 'Đồng ý',
                        cssClass: 'btn-primary',
                        action: function (dialog) {
                            typeof dialog.getData('callback') === 'function' && dialog.getData('callback')(true);
                            dialog.close();
                        }
                    }, {
                        icon: 'glyphicon glyphicon-remove',
                        label: 'Đóng',
                        action: function (dialogItself) {
                            dialogItself.close();
                        }
                    }]
                }).open();
            },
            deleteDataCallback: function (obj) {
                var table = $(obj).closest("table").attr("id");
                var row = $(obj).closest("tr");
                var id = row.attr("id");
                // var myService = "JsonDelete_" + table;
                //$.ColorLifeData.serviceUrl +
                var myService = $.ColorLifeData.serviceUrl + table + "/" + id;
                var dto = { "id": id };
                $.ajax({
                    url: myService,
                    data: { "id": id }, //JSON.stringify(dto),
                    type: "DELETE",//type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            row.remove();
                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
            },
            jsonDeleteData: function (obj) {
                $.ColorLifeData.deleteConfirm("Are you want sure delete?", function (result) {
                    if (result) {
                        $.ColorLifeData.deleteDataCallback(obj);
                    }
                });
                return false;
            },
            jsonAccountDelete: function (obj) {
                $.ColorLifeData.deleteConfirm("Are you want sure delete?", function (result) {
                    if (result) {
                        var row = $(obj).closest("tr");
                        var id = row.attr("id");

                        var myService = "/Account/Delete/" + id;

                        $.ajax({
                            url: myService,

                            type: "DELETE",//type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: true,
                            beforeSend: $.ColorLifeData.loader(true),
                            complete: $.ColorLifeData.loader(false),
                            success: function (result) {
                                if (result.Success) {
                                    row.remove();
                                    $.ColorLifeData.notifySuccess(result.Message);
                                }
                                else {
                                    $.ColorLifeData.notifyError(result.Message);
                                }
                            },
                            error: function (errormessage) {
                                $.ColorLifeData.notifyError(errormessage.responseText);
                            }
                        });
                    }
                });
                return false;
            },

            jsonAccountGetById: function (id, email) {

                $('#myModalSetPassword').modal('show');
                $("#myModalSetPassword").find('#UserId').val(id);
                $("#myModalSetPassword").find('#Email').val(email);
            },
            jsonAccountSetPassword: function (form) {
                var obj = $(form);
                var ChangePasswordModel = {
                    UserId: obj.find("#UserId").val(),
                    Email: obj.find("#Email").val(),

                    Password: obj.find("#Password").val(),
                    ConfirmPassword: obj.find("#ConfirmPassword").val(),
                };
                if (ChangePasswordModel.UserId.length === 0) {
                    $.ColorLifeData.notifyError('You not select user.');
                    obj.find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    return false;
                }
                if (ChangePasswordModel.Password.length === 0 && ChangePasswordModel.ConfirmPassword.length === 0) {
                    $.ColorLifeData.notifyError('Password is required.');
                    obj.find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    return false;
                }
                if (ChangePasswordModel.Password.length < 5 && ChangePasswordModel.ConfirmPassword.length < 5) {
                    $.ColorLifeData.notifyError('Minlength 6 character.');
                    obj.find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    return false;
                }
                if (!$.ColorLifeData.validatePasswordConfirm(ChangePasswordModel.Password, ChangePasswordModel.ConfirmPassword)) {
                    $.ColorLifeData.notifyError('Password not match.');
                    obj.find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    return false;
                }
                $.ajax({
                    url: "/Account/SetUserPassword",
                    data: JSON.stringify(ChangePasswordModel),
                    type: "PUT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            $.ColorLifeData.notifySuccess(result.Message);
                            obj.find("#UserId").val('');
                            obj.find("#Email").val('');
                            obj.find("#Password").val('');
                            obj.find("#ConfirmPassword").val('');
                            obj.find('.form-group').removeClass('has-error has-success');
                            obj.modal('hide');
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
            },
            jsonMoveToTrashData: function (obj) {
                //dele
                $.ColorLifeData.deleteConfirm("Are you sure want delete?", function (result) {
                    if (result) {

                        var table = $(obj).closest("table").attr("id");
                        var row = $(obj).closest("tr");
                        var id = row.attr("id");

                        var dto = { "id": id };

                        $.ajax({
                            // url: $.ColorLifeData.serviceUrl + myService,
                            url: $.ColorLifeData.serviceUrl + table + "/MoveToTrash/" + id,
                            data: JSON.stringify(dto),
                            type: "PUT",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: true,
                            beforeSend: $.ColorLifeData.loader(true),
                            complete: $.ColorLifeData.loader(false),
                            success: function (result) {
                                if (result.Success) {
                                    // state.removeClass(classCurr);
                                    // state.addClass("published-" + x);
                                    row.remove();

                                    $.ColorLifeData.notifySuccess(result.Message);
                                }
                                else {
                                    $.ColorLifeData.notifyError(result.Message);
                                    //  $.ColorLifeData.showStatus("warning", result.Message);
                                }
                            },
                            error: function (errormessage) {
                                //Hiển thị lỗi nếu xảy ra
                                $.ColorLifeData.notifyError(errormessage.responseText);
                            }
                        });
                        return false;

                    } else {

                    }

                });
            },
            jsonRestoreTrashData: function (obj) {
                //dele
                $.ColorLifeData.deleteConfirm("Are you sure you want to delete?", function (result) {
                    if (result) {

                        var table = $(obj).closest("table").attr("id");
                        var row = $(obj).closest("tr");
                        var id = row.attr("id");
                        var dto = { "id": id };
                        $.ajax({
                            // url: $.ColorLifeData.serviceUrl + myService,
                            url: $.ColorLifeData.serviceUrl + table + "/MoveToTrash/" + id,
                            data: JSON.stringify(dto),
                            type: "PUT",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: true,
                            beforeSend: $.ColorLifeData.loader(true),
                            complete: $.ColorLifeData.loader(false),
                            success: function (result) {
                                if (result.Success) {
                                    // state.removeClass(classCurr);
                                    // state.addClass("published-" + x);
                                    row.remove();

                                    $.ColorLifeData.notifySuccess(result.Message);
                                }
                                else {
                                    $.ColorLifeData.notifyError(result.Message);
                                    //  $.ColorLifeData.showStatus("warning", result.Message);
                                }
                            },
                            error: function (errormessage) {
                                //Hiển thị lỗi nếu xảy ra
                                $.ColorLifeData.notifyError(errormessage.responseText);
                            }
                        });
                        return false;

                    } else {

                    }

                });
            },
            jsonMoveToTrashSelectedData: function (obj) {
                var table = $(obj);
                var n = $(".check:checked").length;
                var selectedIds = $(".check:checked");
                if (n == 0) {
                    $.ColorLifeData.notifyError('Select item!');
                    return false;
                }
                else {
                    //dele
                    $.ColorLifeData.deleteConfirm("Are you sure you want to delete?", function (result) {
                        if (result) {
                            selectedIds.each(function () {
                                var id = $(this).val()
                                var dto = { "id": id };
                                var row = $("#" + obj + " > tbody > tr[id=" + JSON.stringify(id) + "]");
                                var myService = obj + "/MoveToTrash/" + id;
                                $.ajax({
                                    url: $.ColorLifeData.serviceUrl + myService,
                                    data: JSON.stringify(dto),
                                    type: "PUT",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    async: true,
                                    //beforeSend: $.ColorLifeData.loader(true),
                                    //complete: $.ColorLifeData.loader(false),
                                    success: function (result) {
                                        if (result.Success) {
                                            row.remove();
                                            // $(".dataTables_info").html(result.Message);
                                            // $.ColorLifeData.notify(true, 'Thành công', result.Message);
                                        }
                                        else {
                                            // $.ColorLifeData.notify(false, 'Lỗi', result.Message);
                                            //  $.ColorLifeData.showStatus("warning", result.Message);
                                        }
                                    },
                                    error: function (errormessage) {
                                        //Hiển thị lỗi nếu xảy ra
                                        // $.ColorLifeData.notify(false, 'Lỗi', errormessage.responseText);
                                    }
                                });
                            });

                        }
                    });

                    return false;
                }
            },
            jsonRestoreSelectedData: function (obj) {
                var table = $(obj);
                var n = $(".check:checked").length;
                var selectedIds = $(".check:checked");
                if (n == 0) {
                    $.ColorLifeData.notifyError('Vui lòng chọn dòng cần khôi phục!');
                    return false;
                }
                else {
                    //dele
                    $.ColorLifeData.deleteConfirm("Bạn chắc chắn muốn khôi phục mục đã chọn?", function (result) {
                        if (result) {
                            selectedIds.each(function () {
                                var id = $(this).val()
                                var dto = { "id": id };
                                var row = $("#" + obj + " > tbody > tr[id=" + JSON.stringify(id) + "]");
                                var myService = obj + "/MoveToTrash/" + id;
                                $.ajax({
                                    url: $.ColorLifeData.serviceUrl + myService,
                                    data: JSON.stringify(dto),
                                    type: "PUT",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    async: true,
                                    //beforeSend: $.ColorLifeData.loader(true),
                                    //complete: $.ColorLifeData.loader(false),
                                    success: function (result) {
                                        if (result.Success) {
                                            row.remove();
                                            // $(".dataTables_info").html(result.Message);
                                            // $.ColorLifeData.notify(true, 'Thành công', result.Message);
                                        }
                                        else {
                                            // $.ColorLifeData.notify(false, 'Lỗi', result.Message);
                                            //  $.ColorLifeData.showStatus("warning", result.Message);
                                        }
                                    },
                                    error: function (errormessage) {
                                        //Hiển thị lỗi nếu xảy ra
                                        // $.ColorLifeData.notify(false, 'Lỗi', errormessage.responseText);
                                    }
                                });
                            });

                        }
                    });

                    return false;
                }
            },
            jsonDeleteSelectedData: function (obj) {
                var table = $(obj);
                var n = $(".check:checked").length;
                var selectedIds = $(".check:checked");
                if (n == 0) {
                    $.ColorLifeData.notifyError('Vui lòng chọn dòng cần xóa!');
                    return false;
                }
                else {
                    //dele
                    $.ColorLifeData.deleteConfirm("Bạn chắc chắn muốn xóa mục đã chọn?", function (result) {
                        if (result) {
                            selectedIds.each(function () {
                                var id = $(this).val()
                                var dto = { "id": id };
                                var row = $("#" + obj + " > tbody > tr[id=" + JSON.stringify(id) + "]");
                                var myService = obj + "/" + id;
                                $.ajax({
                                    url: $.ColorLifeData.serviceUrl + myService,
                                    data: JSON.stringify(dto),
                                    type: "DELETE",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    async: true,
                                    //beforeSend: $.ColorLifeData.loader(true),
                                    //complete: $.ColorLifeData.loader(false),
                                    success: function (result) {
                                        if (result.Success) {
                                            row.remove();
                                            // $(".dataTables_info").html(result.Message);
                                            // $.ColorLifeData.notify(true, 'Thành công', result.Message);
                                        }
                                        else {
                                            // $.ColorLifeData.notify(false, 'Lỗi', result.Message);
                                            //  $.ColorLifeData.showStatus("warning", result.Message);
                                        }
                                    },
                                    error: function (errormessage) {
                                        //Hiển thị lỗi nếu xảy ra
                                        // $.ColorLifeData.notify(false, 'Lỗi', errormessage.responseText);
                                    }
                                });
                            });

                        }
                    });

                    return false;
                }
            },
            jsonDeleteAllData: function (obj) {

                $.ColorLife.deleteConfirm("Cảnh báo! Chọn chức năng này sẽ xóa toàn bộ dữ liệu. Bạn chắc chắn muốn xóa tất cả?", function (result) {
                    if (result) {
                        var table = $(obj);
                        $.ajax({
                            // url: $.ColorLife.serviceUrl + myService,
                            url: $.ColorLife.serviceUrl + obj + "/DeleteAll/?id=1&x=1",
                            //data: JSON.stringify(dto),
                            type: "DELETE",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: true,
                            beforeSend: $.ColorLife.loader(true),
                            complete: $.ColorLife.loader(false),
                            success: function (result) {
                                if (result.Success) {
                                    var row = $("#" + obj + " > tbody");
                                    row.empty();
                                    $("#PagingControl1").html('');
                                    $.ColorLife.notifySuccess(result.Message);
                                    console.log();
                                }
                                else {
                                    $.ColorLife.notifyError(result.Message);
                                    console.log(result.Message);
                                }
                            },
                            error: function (errormessage) {
                                //Hiển thị lỗi nếu xảy ra
                                $.ColorLife.notifyError(errormessage.responseText);
                                console.log(errormessage.responseText);
                            }
                        });
                    };
                });
                return false;
            },
            jsonIsFeatureData: function (obj) {
                var table = $(obj).closest("table").attr("id");
                var row = $(obj).closest("tr");
                var id = row.attr("id");
                var state = $(obj).closest("span");
                var classCurr = state.attr("class");
                var published = row.attr("data-ispublish");

                var x = (published == "True") ? "False" : "True";
                var dto = { "id": id };


                $.ajax({
                    // url: $.ColorLifeData.serviceUrl + myService,
                    url: $.ColorLifeData.serviceUrl + table + "/PutIsFeature/" + id,
                    data: JSON.stringify(dto),
                    type: "PUT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            state.removeClass(classCurr);
                            state.addClass("published-" + x);


                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                            //  $.ColorLifeData.showStatus("warning", result.Message);
                        }
                    },
                    error: function (errormessage) {
                        //Hiển thị lỗi nếu xảy ra
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                }); return false;
            },
            jsonIsPublishedData: function (obj) {
                var table = $(obj).closest("table").attr("id");
                var row = $(obj).closest("tr");
                var id = row.attr("id");
                var state = $(obj).closest("span");
                var classCurr = state.attr("class");
                var published = row.attr("data-ispublish");

                var x = (published == "True") ? "False" : "True";
                var dto = { "id": id };

                var myService = "Json" + table + "_IsPublished";
                $.ajax({
                    // url: $.ColorLifeData.serviceUrl + myService,
                    url: $.ColorLifeData.serviceUrl + table + "/PutIsPublished/" + id,
                    data: JSON.stringify(dto),
                    type: "PUT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            state.removeClass(classCurr);
                            state.addClass("published-" + x);
                            row.attr("data-ispublish", x);

                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                            //  $.ColorLifeData.showStatus("warning", result.Message);
                        }
                    },
                    error: function (errormessage) {
                        //Hiển thị lỗi nếu xảy ra
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                }); return false;
            },
            jsonSortOrderData: function (obj, number) {
                var table = $(obj).closest("table").attr("id");
                var row = $(obj).closest("tr");
                var id = row.attr("id");
                //   var myService = "Json" + table + "_SortOrder";
                var sort = $(obj).closest("span");
                var dto = { "id": id, "number": number };
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + table + "/PutSortOrder?id=" + id + "&number=" + number,
                    //data: JSON.stringify(dto),
                    type: "PUT",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {

                            $.ColorLifeData.notifySuccess(result.Message);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                }); return false;

            },
            jsonSettingUpdate: function (form) {
                var obj = $(form);
                var Setting = {
                    SettingID: $(obj).attr("id"),
                    Name: $(obj).attr("id"),
                    Value: $(obj).val(),
                };

                if (Setting.Value.length > 0) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "Setting",
                        data: JSON.stringify(Setting),
                        type: "PUT",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonEditableSetting: function () {
                $('#Setting a.editable').editable({
                    type: 'text',
                    name: 'Name',

                    title: 'Nhập vào giá trị',
                    // mode: 'inline', // popup or inline
                    ajaxOptions: {
                        type: 'put',
                        dataType: 'json'
                    },
                    validate: function (value) {
                        if ($.trim(value) == '') {
                            return 'This field is required';
                        }
                    },
                    url: function (params) {

                        var Setting = {
                            Name: params.pk,
                            Value: params.value,

                        };
                        return $.ajax({
                            type: 'PUT',
                            url: $.ColorLifeData.serviceUrl + 'Setting',
                            data: JSON.stringify(Setting),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            async: true,
                            cache: false,
                            timeout: 10000,
                            beforeSend: $.ColorLifeData.loader(true),
                            complete: $.ColorLifeData.loader(false),
                            success: function (result) {
                                if (result.Success) {
                                    $.ColorLifeData.notifySuccess(result.Message);
                                }
                                else {
                                    $.ColorLifeData.notifyError(result.Message);
                                }
                            },
                            error: function (errormessage) {
                                $.ColorLifeData.notifyError(errormessage.responseText);
                            }
                        });
                    }
                });
            },

            jsonAddNewCuuSinhVien: function (obj) {
                //dele
                $.ColorLifeData.deleteConfirm("Bạn chắc chắn muốn đưa sinh viên vào cựu sinh viên?", function (result) {
                    if (result) {

                        var table = $(obj).closest("table").attr("id");
                        var row = $(obj).closest("tr");
                        var id = row.attr("id");

                        var dto = { "id": id };

                        $.ajax({

                            url: $.ColorLifeData.serviceUrl + table + "/AddCuuSinhVien/" + id,
                            data: JSON.stringify(dto),
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: true,
                            beforeSend: $.ColorLifeData.loader(true),
                            complete: $.ColorLifeData.loader(false),
                            success: function (result) {
                                if (result.Success) {

                                    row.remove();

                                    $.ColorLifeData.notifySuccess(result.Message);
                                }
                                else {
                                    $.ColorLifeData.notifyError(result.Message);

                                }
                            },
                            error: function (errormessage) {

                                $.ColorLifeData.notifyError(errormessage.responseText);
                            }
                        });
                        return false;

                    } else {

                    }

                });
            },

            jsonInsertNotification: function (form) {

                var obj = $(form);
                var MyNotification = {
                    Name: $('#data-user-info').text(),
                    Message: obj.find("#MyNotiMessage").val(),
                };

                // alert(NhomHangHoa.TenNhomHangHoa);
                if (MyNotification.Message.length && MyNotification.Message.length > 3) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "MyNotification",
                        data: JSON.stringify(MyNotification),
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);
                                $("#MyNotiMessage").val('');
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $.ColorLifeData.notifyError('Bạn cần nhập tin nhắn đầy đủ.');
                }
                return false;

            },

             
            // 22/11/2014
            jsonStaffCheckEmailValidate: function (id, handler) {
                if (id.length) {
                    $.ajax({
                        url: $.ColorLife.serviceUrl + "Staff/CheckEmailAvailable/" + id,

                        type: "GET",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",

                        success: function (result) {
                            handler(result);

                        },
                        error: function (errormessage) {

                            alert(errormessage.responseText);

                        }
                    });
                    return false;
                }
                return false;
            },

            // 17/11/2014
            jsonGetDataObject: function (id, url, handler) {
                $.ajax({
                    type: "GET",
                    url: $.ColorLife.serviceUrl + url + "/" + id,
                    // data: JSON.stringify(dto),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        // $(obj).html($.ColorLife.IMGLoading);
                    },
                    success: function Success(result, status) {
                        handler(result);
                    },
                    error: function Error(request, status, error) {
                        //  $(obj).html(request.statusText);
                        return null;
                    }
                });
            },
            jsonGetDataList: function (id, url, handler) {
                $.ajax({
                    type: "GET",
                    url: $.ColorLife.serviceUrl + url + "/" + id,
                    // data: JSON.stringify(dto),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        // $(obj).html($.ColorLife.IMGLoading);
                    },
                    success: function Success(result, status) {

                        handler(result);
                    },
                    error: function Error(request, status, error) {
                        //  $(obj).html(request.statusText);
                        return null;
                    }
                });
            },
            jsonGetProductImageById: function (id) {
                $.ColorLifeData.jsonGetDataList(id, 'ProductImage/GetByProduct', function (handler) {
                    var html = '<ul class="ace-thumbnails clearfix">';
                    $.each(handler, function (i, item) {
                        html += ' <li>';
                        html += ' <a data-rel="colorbox" href="' + item.ImageUrl + '" class="cboxElement">';
                        html += '<img width="150" height="150" src="' + item.ImageUrl + '" alt="150x150">';
                        html += '<div class="text"><div class="inner">Click để xem</div></div>';
                        html += '</a>';
                        html += ' <div class="tools tools-bottom">';
                        html += '<a href="#" onclick="return $.ColorLifeData.jsonDeleteProductImage(' + item.ImageID + ',' + item.ProductID + ')">';
                        html += '   <i class="ace-icon fa fa-times red"></i>';
                        html += '</a>';
                        html += '   </div>';
                        html += ' </li>';
                    });
                    html += '</ul>';
                    $("#ProductImage").html(html);
                });
            },
            jsonInsertProductImage: function (productId, imageUrl) {
                // var dto = { "id": id, "roleId": rId };
                var ProductImage = {
                    ImageID: -1,
                    ProductID: productId,
                    ImageUrl: imageUrl
                };
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ProductImage",
                    data: JSON.stringify(ProductImage),
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            $.ColorLifeData.notifySuccess(result.Message);
                            $.ColorLifeData.jsonGetProductImageById(productId);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            jsonDeleteProductImage: function (id, productId) {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ProductImage/" + id,
                    type: "DELETE",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            $.ColorLifeData.notifySuccess(result.Message);
                            $.ColorLifeData.jsonGetProductImageById(productId);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            // 03/08/2014
           
            jsonGetAllFileIconMenu: function () {

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "FileIconMenu/LoadAllMenuIcon?path=/content/icons/",
                    //data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            $("#datafileicon").html(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
                return false;
            },
            
       
            jsonPhongChiTietModal_openModal: function (id) {
                var obj = $('#myPhongChiTietModal');

                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "Phong/" + id,
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            obj.find('.modal-title').html('Phòng ' + result.Data.Phong.Ten);
                            obj.find('.tenphong').html(result.Data.Phong.Ten);
                            obj.find('.tenkhunha').html(result.Data.Phong.KhuNha.Ten);
                            obj.find('.loaiphong').html(result.Data.Phong.GetLoaiPhong + ' - ' + result.Data.Phong.GetLoaiPhongSuDung);
                            obj.find('.tinhtrangphong').html(result.Data.Phong.GetTrangThaiPhong);
                            obj.find('.sogiuong').html(result.Data.Phong.SoGiuong);
                            obj.find('.ghichu').html(result.Data.Phong.GhiChu);

                            obj.find('.badge').html(result.Data.HoSos.length);
                            var table = '<table class="table table-hover table-nomargin dataTable table-bordered">'
                            table += "<tr>";
                            table += "<th>MSSV</th>";
                            table += "<th>Họ tên / Giới tính</th>";
                            table += "<th>Ngày sinh</th>";
                            table += "<th>Email/Số ĐT</th>";
                            table += "<th>Ngào vào / ra</th>";
                            table += "<th>Lớp</th>";

                            table += "<th></th>";
                            table += "</tr>";
                            var list = '';
                            $.each(result.Data.HoSos, function (i, value) {
                                list += "<tr>"
                                list += '<td>' + value.MaSoCaNhan + '</td>';
                                list += '<td><a href="../QLSinhVien/Default?controller=CTSinhVien&ID=' + value.MaSoCaNhan + '">' + value.HoTenDayDu + '</a> (' + value.GioiTinh + ')</td>';
                                list += '<td>' + value.NgaySinh + '</br>CMND: ' + value.SoCMND + '</td>';
                                list += '<td>' + value.Email + '</br>' + value.SoDienThoai + '</td>';
                                list += '<td>' + value.NgayBatDau + '</br>' + value.NgayKetThuc + '</td>';
                                list += '<td>' + value.Lop + '</td>';

                                list += '<td><a href="../Phong/Default?controller=DoiPhong&MSCN=' + value.MaSoCaNhan + '" title="Đổi phòng" rel="tooltip"><i class="fa fa-refresh"></i></a><a href="../Phong/Default?controller=TraPhong&MSCN=' + value.MaSoCaNhan + '" title="Trả phòng" rel="tooltip"><i class="fa fa-times"></i></a></td>';
                                list += "</tr>"
                            });
                            table += list;
                            table += '</table>';
                            obj.find('#danhSach').html(table);

                            var dienNuoc = '<table class="table table-hover table-nomargin dataTable table-bordered">'
                            dienNuoc += "<tr>";
                            dienNuoc += "<th>Kỳ SD</th>";
                            dienNuoc += "<th>Loại HD</th>";

                            dienNuoc += "<th>CS cũ</th>";
                            dienNuoc += "<th>Cs mới</th>";
                            dienNuoc += "<th>Tổng mức</th>";
                            dienNuoc += "<th>Đơn giá</th>";
                            dienNuoc += "<th>Thành tiền</th>";
                            dienNuoc += "<th>Trạng thái</th>";

                            dienNuoc += "<th></th>";
                            dienNuoc += "</tr>";
                            var list1 = '';
                            $.each(result.Data.DSHoaDonDienNuocs, function (i, value) {
                                list1 += "<tr>";
                                list1 += '<td>' + value.Thang + '/' + value.Nam + '</td>';
                                list1 += '<td>' + value.GetLoaiHoaDon + '</td>';

                                list1 += '<td>' + value.GetChiSoCu + '</td>';
                                list1 += '<td>' + value.GetChiSoMoi + '</td>';
                                list1 += '<td>' + value.GetTongMucTieuThu + '</td>';
                                list1 += '<td>' + value.DonGia + '</td>';
                                list1 += '<td>' + value.ThanhTien + '</td>';
                                list1 += '<td>' + value.GetTrangThai + '</td>';

                                list1 += '<td><a href="../HoaDon/Default?controller=CTHDTienDienNuoc&id=' + value.IDHoaDon + '" title="Chi tiết" rel="tooltip"><i class="fa fa-edit"></i></a></td>';
                                list1 += "</tr>"
                            });
                            dienNuoc += list1;
                            dienNuoc += '</table>';

                            dienNuoc += '<a href="Default.aspx?controller=ThemHoaDon&IDPhong=' + result.Data.Phong.IDPhong + '" class="btn btn-primary pull-right">Thêm điện nước</a>';
                            dienNuoc += '</br>';
                            obj.find('#dienNuoc').html(dienNuoc);

                            obj.find('.linkchitiet').attr('href', 'Default.aspx?controller=CTPhong&IDPhong=' + result.Data.Phong.IDPhong);
                            $(obj).modal('show');
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });

            },
            jsonUpdateHitsData: function (obj) {
                var table = $(obj).attr("data-table");
                var id = $(obj).attr("data-id");
                var myService = "Json" + table + "_UpdateHits";
                var dto = { "id": id };
                $.ajax({
                    url: $.ColorLife.serviceUrl + myService,
                    data: JSON.stringify(dto),
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,

                    success: function (result) {
                        if (result.Success) {
                            // alert(result.Message);
                            //$.ColorLife.notify(true, 'Thành công', result.Message);
                        }
                        else {
                            // $.ColorLife.notify(false, 'Lỗi', result.Message);
                            //  $.ColorLife.showStatus("warning", result.Message);
                            // alert(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        //Hiển thị lỗi nếu xảy ra
                        // $.ColorLife.notify(false, 'Lỗi', errormessage.responseText);
                        // alert(errormessage.responseText);
                    }
                });
                return false;
            },
            // 21/7/2014 - CRUD
            jsonGetCaiDat: function (id) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "CaiDat/" + id,
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            alert(result.Data.KeyValue);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
            },
            jsonGetCaiDatThongTinCongTy: function (handleData) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "ThongTinCongTy",
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
            },
            jsonUpdateThongTinCongTy: function () {
                var obj = $('#myModalThongTin');
                var ThongTinCongTyModel = {
                    TenCongTy: obj.find("#CTY_TEN").val(),
                    DiaChi: obj.find("#CTY_DIACHI").val(),
                    SoDienThoai: obj.find("#CTY_SODT").val(),
                    Fax: obj.find("#CTY_FAX").val(),
                    Website: obj.find("#CTY_WEBSITE").val(),
                    Email: obj.find("#CTY_EMAIL").val(),
                    LinhVuc: obj.find("#CTY_LINHVUC").val(),
                    MST: obj.find("#CTY_MST").val(),
                    Logo: obj.find("#CTY_LOGO").val(),
                    GPKD: obj.find("#CTY_GPKD").val(),
                };
                if (ThongTinCongTyModel.TenCongTy.length && ThongTinCongTyModel.DiaChi.length > 3) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "ThongTinCongTy",
                        data: JSON.stringify(ThongTinCongTyModel),
                        type: "PUT",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                        },
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);
                                //  $('#myModalThongTin').modal('hide');
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonGetCaiDatMaTuDong: function (handleData) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "MaTuDong",
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            handleData(result.Data);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
            },
            jsonUpdateMaTuDong: function () {
                var obj = $('#myModalMaTuDong');
                var MaTuDongModel = {
                    HangHoa: obj.find("#MA_HH").val(),
                    KhachHang: obj.find("#MA_KH").val(),
                    NhaCungCap: obj.find("#MA_NCC").val(),
                    PhieuNhap: obj.find("#MA_PN").val(),
                    PhieuXuat: obj.find("#MA_PX").val(),
                    PhieuChuyenKho: obj.find("#MA_PCH").val(),
                    PhieuNhapTra: obj.find("#MA_PNT").val(),
                    PhieuXuatTra: obj.find("#MA_PXT").val(),
                    PhieuThu: obj.find("#MA_PT").val(),
                    PhieuChi: obj.find("#MA_PC").val(),
                    PhieuThuKhac: obj.find("#MA_PTK").val(),
                    PhieuChiKhac: obj.find("#MA_PCK").val(),
                };
                if (MaTuDongModel.HangHoa.length && MaTuDongModel.KhachHang.length) {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "MaTuDong",
                        data: JSON.stringify(MaTuDongModel),
                        type: "PUT",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                        },
                        success: function (result) {
                            if (result.Success) {
                                $.ColorLifeData.notifySuccess(result.Message);
                                //  $('#myModalThongTin').modal('hide');
                            }
                            else {
                                $.ColorLifeData.notifyError(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
                else {
                    $(obj).find('.form-group').removeClass('has-error has-success').addClass('has-error');
                    $.ColorLifeData.notifyError('Bạn cần nhập dữ liệu đầy đủ.');
                }
                return false;
            },
            jsonEditableCaiDat: function () {
                $('#CaiDat a.editable').editable({
                    type: 'text',
                    name: 'KeyName',

                    title: 'Nhập vào giá trị',
                    // mode: 'inline', // popup or inline
                    ajaxOptions: {
                        type: 'put',
                        dataType: 'json'
                    },
                    validate: function (value) {
                        if ($.trim(value) == '') {
                            return 'This field is required';
                        }
                    },
                    url: function (params) {

                        var CaiDat = {
                            KeyName: params.pk,
                            KeyValue: params.value,

                        };
                        return $.ajax({
                            type: 'PUT',
                            url: $.ColorLifeData.serviceUrl + 'CaiDat',
                            data: JSON.stringify(CaiDat),
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            async: true,
                            cache: false,
                            timeout: 10000,
                            success: function (result) {
                                if (result.Success) {
                                    $.ColorLifeData.notifySuccess(result.Message);
                                }
                                else {
                                    $.ColorLifeData.notifyError(result.Message);
                                }
                            },
                            error: function (errormessage) {
                                $.ColorLifeData.notifyError(errormessage.responseText);
                            }
                        });
                    }
                });
            },

            jsonGetHoSo: function (id) {
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "HoSo/" + id,
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {
                            alert(result.Data.KeyValue);
                        }
                        else {
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
            },
            jsonValidateMaSoCaNhan: function (id) {
                if (id === '')
                    $.ColorLifeData.notifyError('Vui lòng nhập Mã số cá nhân.');
                else {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "HoSo/GetValidate/" + id,
                        // data: JSON.stringify(dto),
                        type: "GET",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $('.masocanhan').val('');
                                $.ColorLifeData.notifyError(result.Message);
                            }
                            else {
                                $.ColorLifeData.notifyInfo(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
            },
            jsonValidateMaSoSinhVien: function (id) {
                if (id === '')
                    $.ColorLifeData.notifyError('Vui lòng nhập Mã số Sinh viên.');
                else {
                    $.ajax({
                        url: $.ColorLifeData.serviceUrl + "Student/GetValidate/" + id,
                        // data: JSON.stringify(dto),
                        type: "GET",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: $.ColorLifeData.loader(true),
                        complete: $.ColorLifeData.loader(false),
                        success: function (result) {
                            if (result.Success) {
                                $('.masocanhan').val('');
                                $.ColorLifeData.notifyError(result.Message);
                            }
                            else {
                                $.ColorLifeData.notifyInfo(result.Message);
                            }
                        },
                        error: function (errormessage) {
                            $.ColorLifeData.notifyError(errormessage.responseText);
                        }
                    });
                }
            },
            jsonValidateMaSoCaNhanHoaDon: function (id) {
                if (id === '')
                    $.ColorLifeData.notifyError('Vui lòng nhập Mã số cá nhân.');
                $.ajax({
                    url: $.ColorLifeData.serviceUrl + "HoSo/?id=" + id + "&x=1",
                    // data: JSON.stringify(dto),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result.Success) {

                            // $.ColorLifeData.notifyError(result.Message);
                        }
                        else {
                            $('.masocanhan').val('');
                            $.ColorLifeData.notifyError(result.Message);
                        }
                    },
                    error: function (errormessage) {
                        $.ColorLifeData.notifyError(errormessage.responseText);
                    }
                });
            },

            


            mySearchMember: function (key) {
                var searchkey = "";
                var finishsearch = true;
                if (key.length === 0) {
                    searchkey = "";
                    $("#suggestions").fadeOut();
                } else {
                    if (!finishsearch)
                        return;

                    if ($.trim(key) !== searchkey) {
                        searchkey = $.trim(key);
                    } else {
                        return;
                    }
                    finishsearch = false;
                    $.get("/Account/FindUserBy", { q: searchkey }, function (result) { 
                        $(".searchresults").html('');

                        var html = "<ul>";

                        if (result != null && result.length > 0) {
                            $.each(result, function (i, value) {
                                html += '<li  onclick="return addMember(\''+value.Id+'\');">';
                                html += '<a href="#">';
                                html += "<img width='32' src='" + value.Avatar + "' alt='" + value.FullName + "' />";
                                html += "<p><span class='text_show'>" + value.Email + "</span><br/><span class='text_show'>" + value.FullName + "</span></p>";
                                html += "</a></li>";

                            });
                        }

                        html += "</ul>";
                        $(".searchresults").html(html);
                        $("#suggestions").fadeIn();

                        $("#suggestions .text_show").highlight(key);

                        finishsearch = true;

                    }).error(function () {
                        finishsearch = true;
                        $(".searchresults").html("");
                        $("#suggestions").fadeOut();
                    }).complete(function () {
                        finishsearch = true;
                    });
                }
            },

            jsonAddMemberToTeam: function (userId, teamId) {
                var TeamMember = {
                    Id: 1,
                    UserId: userId,
                    TeamId: teamId,
                };
                $.ajax({
                    url: "/api/teammember/",
                    data: JSON.stringify(TeamMember),
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result > 1) {
                            $.ColorLifeData.notifySuccess("Thêm thành viên vào nhóm thành công");
                        }
                        else {
                            $.ColorLifeData.notifyError("Thành viên đã có trong nhóm");
                        }
                    },
                    error: function (errormessage) {
                        // $.ColorLifeData.notifyError(errormessage.responseText);
                        $.ColorLifeData.notifyError("Có lỗi xảy ra, vui lòng thử lại.");
                    }
                }); return false;
            },
            jsonRemoveMemberOfTeam: function (userId, teamId) {
                
                $.ajax({
                    url: "/api/teammember/"+teamId+"?userId="+userId,
                  //  data: JSON.stringify(TeamMember),
                    type: "DELETE",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    beforeSend: $.ColorLifeData.loader(true),
                    complete: $.ColorLifeData.loader(false),
                    success: function (result) {
                        if (result > 0) {
                            $.ColorLifeData.notifySuccess("Xóa thành viên khỏi nhóm thành công");
                        }
                        else {
                            $.ColorLifeData.notifyError("Thành viên đã có trong nhóm");
                        }
                    },
                    error: function (errormessage) {
                        // $.ColorLifeData.notifyError(errormessage.responseText);
                        $.ColorLifeData.notifyError("Có lỗi xảy ra, vui lòng thử lại.");
                    }
                }); return false;
            },
            // , tiep tuc ham khac            
        }// end ColorLife
    }); // end extend
})(jQuery);