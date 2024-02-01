$(function () {
    abp.log.debug('Index.js initialized!');

    var l = abp.localization.getResource('SysResource');
    var invoiceService = eInvoice.invoiceJournals;
    var auditService = sys.auditLogs.auditLog;

    var contextTextColor = "var(--lpx-content-text)";
    var contextBgColor = "rgba(var(--lpx-content-text-rgb), 0.2)";

    var commonChartOptions = {
        legend: {
            labels: {
                colors: contextTextColor
            }
        },
        dataLabels: {
            enabled: false
        },
        grid: {
            show: false,
            xaxis: {
                lines: {
                    show: false
                }
            },
            yaxis: {
                lines: {
                    show: false
                }
            }
        }
    }


    var montlyInvoiceCount = [];

    var isAuthenticated = abp.currentUser.isAuthenticated;

    if (isAuthenticated) {
        //'Today': [moment(), moment()],
        //'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
        //'Last 7 Days': [moment().subtract(6, 'days'), moment()],
        //'Last 30 Days': [moment().subtract(29, 'days'), moment()],
        //'This Month': [moment().startOf('month'), moment().endOf('month')],
        //'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]

        var currentDate = new Date();
        var currentYear = currentDate.getFullYear();
        var currentMonth = currentDate.getMonth() + 1; // Months are zero-based, so we need to add 1
        var currentDay = currentDate.getDate();
        var ToDateString = currentYear + "-0" + currentMonth + "-" + currentDay;
        var firstDayDateString = currentYear + "-01-01";


        var dataTable = $('#ActiveUsers').DataTable(abp.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: false,
            searching: false,
            autoWidth: false,
            scrollCollapse: true,
            ajax: abp.libs.datatables.createAjax(auditService.getActiveUsers, function () {
                return {
                    startDate: firstDayDateString,//moment.utc(startTime),
                    endDate: ToDateString//moment.utc(endTime),
                }
            }),
            columnDefs: [
                { data: "userName" },
                { data: "name" },
                //{ data: "executionTime" },
                {
                    title: "last active",
                    data: "lastModificationTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toFormat("dd-MM-yyyy HH:mm:ss");
                    }
                },
                { data: "accessFailedCount" }
            ]
        }));

        // 8-8-8-8-8-8-8-8-8-8-8-8-
        //   Success Rate chart
        // 8-8-8-8-8-8-8-8-8-8-8-8-
        //debugger
        auditService.getErrorRate({ startDate: firstDayDateString, endDate: ToDateString }).done(function (result) {
            //debugger;
            $("#SucceedsTotal").text(result.data.Success);
            $("#SucceedsToday").text(2);
            $("#FailsTotal").text(result.data.Fault);
            $("#FailsToday").text(1);
        });

        invoiceService.yearlyInvoiceSum().done(function (result) {
            $("#YearlyInvoiceSum").text("$" + result);
        });
        invoiceService.monthlyInvoiceSum().done(function (result) {
            $("#MonthlyInvoiceSum").text("$" + result);
        });
        // monthly invoice count chart
        invoiceService.monthlyInvoiceCount().done(function (result) {
            //debugger;
            montlyInvoiceCount = result;
            var montlyInvoiceCountBarChartOption = {
                series: [
                    {
                        data: montlyInvoiceCount,
                        name: "Monthly"
                    },
                ],
                chart: {
                    type: "bar",
                    zoom: {
                        enabled: false
                    },
                    toolbar: {
                        show: false
                    },
                    width: 100,
                    height: 276
                },
                plotOptions: {
                    bar: {
                        horizontal: false,
                        columnWidth: "32%",
                        borderRadius: 4
                    },
                },
                fill: {
                    colors: ["#ffffff"],
                    opacity: 1
                },
                dataLabels: {
                    enabled: false,
                },
                stroke: {
                    width: 1,
                    colors: ["var(--lpx-primary)"]
                },
                xaxis: {
                    tickAmount: 11,
                    labels: {
                        formatter: function (tickNo) {
                            var e = Math.ceil(tickNo) + "";
                            return 1 === e.length ? "0" + e : e
                        },
                        style: {
                            colors: "white"
                        }
                    },
                    axisBorder: {
                        show: false
                    },
                    axisTicks: {
                        show: false
                    }
                },
                yaxis: {
                    show: false,
                    //min: 0,
                    //max: 10
                },
            };

            montlyInvoiceCountBarChartOption = Object.assign({}, commonChartOptions, montlyInvoiceCountBarChartOption);

            var montlyInvoiceCountBarChart = new ApexCharts(document.querySelector("#barChart"), montlyInvoiceCountBarChartOption);
            montlyInvoiceCountBarChart.render();

            setTimeout((function () {
                montlyInvoiceCountBarChart.updateOptions({
                    chart: {
                        width: "100%"
                    }
                })
            }
            ), 400);

            createSimpleAreaChart({
                id: "#succeed-rate",
                data: [3,6,9,10,11],
                color: "var(--lpx-success)",
                name: "Succeeds"
            });

            createSimpleAreaChart({
                id: "#fails-rate",
                data: [10,5,2,1,1],
                color: "var(--lpx-brand)",
                name: "Balance"
            });

            createRadialChart({
                id: "#annual-limit-radial",
                    percentage: 77,
                        label: "Annual Limit"
            });

            createRadialChart({
                id: "#monthly-limit-radial",
                    percentage: 54,
                        label: "Monthly Limit"
            });

        });



    }// if (isAuthenticated) 

    // Radial Chart
    var createRadialChart = function (param) {
        var radialChartOption = {
            series: [param.percentage],
            chart: {
                width: 100,
                height: 130,
                type: "radialBar"
            },
            plotOptions: {
                radialBar: {
                    hollow: {
                        margin: 0,
                        size: "60%"
                    },
                    track: {
                        dropShadow: {
                            enabled: !0,
                            top: 2,
                            left: 0,
                            blur: 4,
                            opacity: .15
                        },
                        background: contextBgColor
                    },
                    dataLabels: {
                        name: {
                            show: false
                        },
                        value: {
                            color: "#fff",
                            fontSize: "20px",
                            show: true,
                            offsetY: 10
                        }
                    }
                }
            },
            stroke: {
                lineCap: "round"
            },
            fill: {
                colors: ["#ffffff"]
            }
        };

        radialChartOption = Object.assign({}, commonChartOptions, radialChartOption);

        var radialChart = new ApexCharts(document.querySelector(param.id), radialChartOption);
        radialChart.render();

        setTimeout((function () {
            radialChart.updateOptions({
                chart: {
                    width: "100%"
                }
            })
        }
        ), 400);
    }

    //Area Chart
    var createSimpleAreaChart = function (t)
    {
        var areaChartOptions = {
            colors: [t.color],
            series: [{
                data: t.data,
                name: t.name
            }],
            chart: {
                type: "area",
                width: 100,
                height: 86,
                zoom: {
                    enabled: false
                },
                toolbar: {
                    show: false
                }
            },
            stroke: {
                curve: "smooth",
                width: 4
            },
            fill: {
                type: "gradient",
                gradient: {
                    opacityFrom: .25,
                    opacityTo: 0,
                    stops: [0, 100],
                    colorStops: [[{
                        offset: 0,
                        color: t.color,
                        opacity: .25
                    }, {
                        offset: 100,
                        color: "var(--lpx-card-bg)",
                        opacity: .15
                    }]],
                    shadeIntensity: 1
                }
            },
            dataLabels: {
                enabled: false
            },
            yaxis: {
                show: false,
                min: 0,
                max: 20
            },
            xaxis: {
                labels: {
                    show: false
                },
                offsetY: 50
            }
        }
        areaChartOptions = Object.assign({}, commonChartOptions, areaChartOptions);

        var areaChart = new ApexCharts(document.querySelector(t.id), areaChartOptions);
        areaChart.render();

        setTimeout((function () {
            areaChart.updateOptions({
                chart: {
                    width: "100%"
                }
            })
        }
        ), 400);
    }

});