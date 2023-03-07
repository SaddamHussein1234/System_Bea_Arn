﻿var EmployeeSalaryProcessSave = ({
    Control: {
        txtPaidDate: ".txtPaidDate",
        hfCalculateSalary: "#hfCalculateSalary",
        lblOnHandSalary: ".lblOnHandSalary",
        lblTotalPaidLoanAmount: ".lblTotalPaidLoanAmount",
        txtTotalPaidLoanAmount: ".txtTotalPaidLoanAmount",
        txtPaidLoanAmount: ".txtPaidLoanAmount",
        txtOnHandSalary: ".txtOnHandSalary",
    },

    checkPaidLoanAmount: function (el) {
       
        var PaidTotalLoan = 0;
        var OldPaidLoanAmount = 0;
        var PaidLoanAmount = parseFloat($(el).val());
        var PendingLoanAmount = parseFloat($(el).attr("Amount"));

        if (PaidLoanAmount > PendingLoanAmount) {
            $(el).val(PendingLoanAmount);
        }

        $(EmployeeSalaryProcessSave.Control.txtTotalPaidLoanAmount).each(function () {
            if ($(this).val() != '') {
                if (!isNaN(parseFloat($(this).val()))) {
                    OldPaidLoanAmount = OldPaidLoanAmount + parseFloat($(this).val());
                }
            }
        });

        $(EmployeeSalaryProcessSave.Control.txtPaidLoanAmount).each(function () {
            if ($(this).val() != '') {
                if (!isNaN(parseFloat($(this).val()))) {
                    PaidTotalLoan = PaidTotalLoan + parseFloat($(this).val());
                }
            }
        });
        
        $(EmployeeSalaryProcessSave.Control.lblTotalPaidLoanAmount).text((PaidTotalLoan).toFixed(2));
        $(EmployeeSalaryProcessSave.Control.lblOnHandSalary).text(($(EmployeeSalaryProcessSave.Control.hfCalculateSalary).val() - PaidTotalLoan + OldPaidLoanAmount).toFixed(2));
        
        $(EmployeeSalaryProcessSave.Control.txtOnHandSalary).val($(EmployeeSalaryProcessSave.Control.lblOnHandSalary).text());
    },
});

$(document).ready(function () {
    $(EmployeeSalaryProcessSave.Control.txtPaidDate).daterangepicker({
        singleDatePicker: true
    });
});
