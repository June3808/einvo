var leptonxDemoDashboard;
(() => {
    var t = {
        522: function (t, e, a) {
            var r, o, n = this && this.__assign || function () {
                return n = Object.assign || function (t) {
                    for (var e, a = 1, r = arguments.length; a < r; a++)
                        for (var o in e = arguments[a])
                            Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
                    return t
                }
                    ,
                    n.apply(this, arguments)
            }
                , i = this && this.__importDefault || function (t) {
                    return t && t.__esModule ? t : {
                        default: t
                    }
                }
                ;
            r = [a, e, a(161), a(549)],
                o = function (t, e, a, r) {
                    "use strict";
                    Object.defineProperty(e, "__esModule", {
                        value: !0
                    }),
                        e.createAreaChart = void 0,
                        a = i(a),
                        e.createAreaChart = function (t) {
                            var e = n(n({}, r.commonChartOptions), {
                                colors: t.colors,
                                series: t.series,
                                chart: {
                                    type: "area",
                                    width: 100,
                                    height: 320,
                                    zoom: {
                                        enabled: !1
                                    },
                                    toolbar: {
                                        show: !1
                                    }
                                },
                                stroke: {
                                    curve: "smooth",
                                    width: 4
                                },
                                yaxis: {
                                    show: !1,
                                    min: 0,
                                    max: 100
                                },
                                xaxis: {
                                    labels: {
                                        show: !1,
                                        style: {
                                            colors: r.contextTextColor
                                        },
                                        formatter: function (t) {
                                            return t && t.split(" ")[0]
                                        }
                                    },
                                    axisTicks: {
                                        show: !1
                                    },
                                    axisBorder: {
                                        show: !1
                                    }
                                },
                                fill: {
                                    type: "gradient",
                                    gradient: {
                                        opacityFrom: .5,
                                        opacityTo: 0,
                                        stops: [0, 100],
                                        colorStops: [[{
                                            offset: 0,
                                            color: t.colors[0],
                                            opacity: .25
                                        }, {
                                            offset: 100,
                                            color: "var(--lpx-card-bg)",
                                            opacity: .15
                                        }], [{
                                            offset: 0,
                                            color: t.colors[1],
                                            opacity: .25
                                        }, {
                                            offset: 100,
                                            color: "var(--lpx-card-bg)",
                                            opacity: .15
                                        }]],
                                        shadeIntensity: 1
                                    }
                                }
                            })
                                , o = new a.default(document.querySelector(t.id), e);
                            return o.render(),
                                setTimeout((function () {
                                    o.updateOptions({
                                        chart: {
                                            width: "100%"
                                        }
                                    })
                                }
                                ), 400),
                                o
                        }
                }
                    .apply(e, r),
                void 0 === o || (t.exports = o)
        },
        163: function (t, e, a) {
            var r, o, n = this && this.__assign || function () {
                return n = Object.assign || function (t) {
                    for (var e, a = 1, r = arguments.length; a < r; a++)
                        for (var o in e = arguments[a])
                            Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
                    return t
                }
                    ,
                    n.apply(this, arguments)
            }
                , i = this && this.__importDefault || function (t) {
                    return t && t.__esModule ? t : {
                        default: t
                    }
                }
                ;
            r = [a, e, a(161), a(549)],
                o = function (t, e, a, r) {
                    "use strict";
                    Object.defineProperty(e, "__esModule", {
                        value: !0
                    }),
                        e.createBarChart = void 0,
                        a = i(a),
                        e.createBarChart = function (t) {
                            var e = n(n({}, r.commonChartOptions), {
                                series: [{
                                    data: t.data,
                                    name: t.name
                                }],
                                chart: {
                                    type: "bar",
                                    zoom: {
                                        enabled: !1
                                    },
                                    toolbar: {
                                        show: !1
                                    },
                                    width: 100,
                                    height: 276
                                },
                                stroke: {
                                    width: 1,
                                    colors: ["var(--lpx-primary)"]
                                },
                                yaxis: {
                                    show: !1,
                                    min: 0,
                                    max: 10
                                },
                                xaxis: {
                                    tickAmount: 11,
                                    labels: {
                                        formatter: function (t) {
                                            var e = Math.ceil(+t + 1) + "";
                                            return 1 === e.length ? "0" + e : e
                                        },
                                        style: {
                                            colors: "white"
                                        }
                                    },
                                    axisBorder: {
                                        show: !1
                                    },
                                    axisTicks: {
                                        show: !1
                                    }
                                },
                                plotOptions: {
                                    bar: {
                                        horizontal: !1,
                                        columnWidth: "32%",
                                        borderRadius: 4
                                    }
                                },
                                fill: {
                                    colors: ["#ffffff"],
                                    opacity: 1
                                }
                            })
                                , o = new a.default(document.querySelector(t.id), e);
                            o.render(),
                                setTimeout((function () {
                                    o.updateOptions({
                                        chart: {
                                            width: "100%"
                                        }
                                    })
                                }
                                ), 400)
                        }
                }
                    .apply(e, r),
                void 0 === o || (t.exports = o)
        },
        359: (t, e, a) => {
            var r, o;
            r = [a, e, a(522), a(163), a(549), a(935), a(50), a(822), a(734)],
                o = function (t, e, a, r, o, n, i, s, l) {
                    "use strict";
                    Object.defineProperty(e, "__esModule", {
                        value: !0
                    }),
                        e.dashboard = e.Dashboard = void 0;
                    var c = function () {
                        function t() {
                            var t = this;
                            this.charts = [],
                                this.createRandomDataBetween = function (t, e, a, r) {
                                    return void 0 === r && (r = function (t) {
                                        return t
                                    }
                                    ),
                                        new Array(a).fill(0).map((function (a, o) {
                                            return {
                                                x: r(o),
                                                y: Math.floor(Math.random() * (e - t + 1)) + t
                                            }
                                        }
                                        ))
                                }
                                ;
                            var e = document.querySelector("#revenue-tab");
                            e.addEventListener("shown.bs.tab", (function (a) {
                                var r, o = (r = e.querySelector(".nav-link.active"),
                                    Array.from(e.querySelectorAll(".nav-link")).indexOf(r));
                                t.charts[o] && t.charts[o].destroy(),
                                    t.createRevenueChart(o)
                            }
                            ))
                        }
                        return t.init = function () {
                            return (new t).withRadialCharts()
                        }
                            ,
                            t.prototype.createRevenueChart = function (t) {
                                var e = function (t) {
                                    var e = new Date;
                                    return e.setMonth(t),
                                        e.setDate(1),
                                        e.toLocaleDateString("en-US", {
                                            month: "short",
                                            day: "numeric"
                                        })
                                }
                                    , r = [{
                                        id: "#revenue-line2",
                                        name: "rerender",
                                        colors: ["var(--lpx-primary)", "var(--lpx-brand)"],
                                        series: [{
                                            data: this.createRandomDataBetween(5, 50, 5, e),
                                            name: "Revenue"
                                        }, {
                                            data: this.createRandomDataBetween(30, 80, 5, e),
                                            name: "Expenses"
                                        }]
                                    }, {
                                        id: "#revenue-line3",
                                        name: "rerender",
                                        colors: ["var(--lpx-primary)", "var(--lpx-brand)"],
                                        series: [{
                                            data: this.createRandomDataBetween(5, 50, 5, e),
                                            name: "Revenue"
                                        }, {
                                            data: this.createRandomDataBetween(30, 80, 5, e),
                                            name: "Expenses"
                                        }]
                                    }, {
                                        id: "#revenue-line4",
                                        name: "rerender",
                                        colors: ["var(--lpx-primary)", "var(--lpx-brand)"],
                                        series: [{
                                            data: this.createRandomDataBetween(40, 100, 5, e),
                                            name: "Revenue"
                                        }, {
                                            data: this.createRandomDataBetween(20, 80, 5, e),
                                            name: "Expenses"
                                        }]
                                    }];
                                this.charts[t] = (0,
                                    a.createAreaChart)(r[t])
                            }
                            ,
                            t.prototype.withRadialCharts = function () {
                                return [{
                                    id: "#annual-limit-radial",
                                    percentage: 77,
                                    label: "Annual Limit"
                                }, {
                                    id: "#monthly-limit-radial",
                                    percentage: 54,
                                    label: "Monthly Limit"
                                }].forEach(s.createRadialChart),
                                    [{
                                        id: "#earnings-line",
                                        data: this.createRandomDataBetween(5, 20, 5),
                                        color: "var(--lpx-success)",
                                        name: "Earning"
                                    }, {
                                        id: "#balance-line",
                                        data: this.createRandomDataBetween(5, 20, 5),
                                        color: "var(--lpx-brand)",
                                        name: "Balance"
                                    }].forEach(l.createSimpleAreaChart),
                                    this.createRevenueChart(0),
                                    [{
                                        id: "#annual-bar",
                                        data: this.createRandomDataBetween(3, 10, 12),
                                        name: "Annual",
                                        colors: ["var(--lpx-success)", o.contextBgColor]
                                    }].forEach(r.createBarChart),
                                    [{
                                        id: "#monthly-donut",
                                        data: [82.3, 17.7],
                                        name: "Monthly Goal",
                                        colors: ["var(--lpx-success)", o.contextBgColor]
                                    }].forEach(n.createDonutChart),
                                    [{
                                        id: "#total-earning-radialbar",
                                        data: [865, 654, 778].map((function (t) {
                                            return Math.round(t / 2550 * 100)
                                        }
                                        )),
                                        name: "Total Earning",
                                        colors: ["var(--lpx-success)", o.contextBgColor],
                                        labels: ["Presentation", "Development", "Design"]
                                    }].forEach(i.createMultipleRadialBarChart),
                                    this
                            }
                            ,
                            t
                    }();
                    e.Dashboard = c,
                        leptonx.init.push("dashboardInit", (function () {
                            e.dashboard = c.init()
                        }
                        ))
                }
                    .apply(e, r),
                void 0 === o || (t.exports = o)
        }
        ,
        549: (t, e, a) => {
            var r;
            r = function (t, e) {
                "use strict";
                Object.defineProperty(e, "__esModule", {
                    value: !0
                }),
                    e.commonChartOptions = e.contextBgColor = e.contextTextColor = void 0,
                    e.contextTextColor = "var(--lpx-content-text)",
                    e.contextBgColor = "rgba(var(--lpx-content-text-rgb), 0.2)",
                    e.commonChartOptions = {
                        legend: {
                            labels: {
                                colors: e.contextTextColor
                            }
                        },
                        dataLabels: {
                            enabled: !1
                        },
                        grid: {
                            show: !1,
                            xaxis: {
                                lines: {
                                    show: !1
                                }
                            },
                            yaxis: {
                                lines: {
                                    show: !1
                                }
                            }
                        }
                    }
            }
                .apply(e, [a, e]),
                void 0 === r || (t.exports = r)
        }
        ,
        935: function (t, e, a) {
            var r, o, n = this && this.__assign || function () {
                return n = Object.assign || function (t) {
                    for (var e, a = 1, r = arguments.length; a < r; a++)
                        for (var o in e = arguments[a])
                            Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
                    return t
                }
                    ,
                    n.apply(this, arguments)
            }
                , i = this && this.__importDefault || function (t) {
                    return t && t.__esModule ? t : {
                        default: t
                    }
                }
                ;
            r = [a, e, a(161), a(549)],
                o = function (t, e, a, r) {
                    "use strict";
                    Object.defineProperty(e, "__esModule", {
                        value: !0
                    }),
                        e.createDonutChart = void 0,
                        a = i(a),
                        e.createDonutChart = function (t) {
                            var e = n(n({}, r.commonChartOptions), {
                                series: t.data,
                                chart: {
                                    type: "donut",
                                    width: 100
                                },
                                plotOptions: {
                                    pie: {
                                        startAngle: -90,
                                        endAngle: 90,
                                        offsetY: 10,
                                        donut: {
                                            labels: {
                                                show: !1,
                                                name: {
                                                    show: !0
                                                },
                                                total: {
                                                    label: "82"
                                                },
                                                value: {
                                                    show: !0
                                                }
                                            },
                                            size: "85"
                                        }
                                    }
                                },
                                grid: {
                                    padding: {
                                        bottom: -80
                                    }
                                },
                                tooltip: {
                                    enabled: !1
                                },
                                responsive: [{
                                    breakpoint: 480,
                                    options: {
                                        chart: {
                                            width: 200
                                        },
                                        legend: {
                                            position: "bottom"
                                        }
                                    }
                                }],
                                legend: {
                                    show: !1
                                },
                                colors: t.colors,
                                stroke: {
                                    show: !1
                                }
                            })
                                , o = new a.default(document.querySelector(t.id), e);
                            o.render(),
                                setTimeout((function () {
                                    o.updateOptions({
                                        chart: {
                                            width: "100%"
                                        }
                                    })
                                }
                                ), 400)
                        }
                }
                    .apply(e, r),
                void 0 === o || (t.exports = o)
        },
        50: function (t, e, a) {
            var r, o, n = this && this.__assign || function () {
                return n = Object.assign || function (t) {
                    for (var e, a = 1, r = arguments.length; a < r; a++)
                        for (var o in e = arguments[a])
                            Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
                    return t
                }
                    ,
                    n.apply(this, arguments)
            }
                , i = this && this.__importDefault || function (t) {
                    return t && t.__esModule ? t : {
                        default: t
                    }
                }
                ;
            r = [a, e, a(161), a(549)],
                o = function (t, e, a, r) {
                    "use strict";
                    Object.defineProperty(e, "__esModule", {
                        value: !0
                    }),
                        e.createMultipleRadialBarChart = void 0,
                        a = i(a),
                        e.createMultipleRadialBarChart = function (t) {
                            var e = n(n({}, r.commonChartOptions), {
                                series: t.data,
                                chart: {
                                    type: "radialBar",
                                    height: 200,
                                    width: 100
                                },
                                plotOptions: {
                                    radialBar: {
                                        track: {
                                            background: r.contextBgColor,
                                            strokeWidth: "100%",
                                            margin: 3
                                        }
                                    }
                                },
                                stroke: {
                                    lineCap: "round"
                                },
                                labels: t.labels
                            })
                                , o = new a.default(document.querySelector(t.id), e);
                            o.render(),
                                setTimeout((function () {
                                    o.updateOptions({
                                        chart: {
                                            width: "100%"
                                        }
                                    })
                                }
                                ), 400)
                        }
                }
                    .apply(e, r),
                void 0 === o || (t.exports = o)
        },
        822: function (t, e, a) {
            var r, o, n = this && this.__assign || function () {
                return n = Object.assign || function (t) {
                    for (var e, a = 1, r = arguments.length; a < r; a++)
                        for (var o in e = arguments[a])
                            Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
                    return t
                }
                    ,
                    n.apply(this, arguments)
            }
                , i = this && this.__importDefault || function (t) {
                    return t && t.__esModule ? t : {
                        default: t
                    }
                }
                ;
            r = [a, e, a(161), a(549)],
                o = function (t, e, a, r) {
                    "use strict";
                    Object.defineProperty(e, "__esModule", {
                        value: !0
                    }),
                        e.createRadialChart = void 0,
                        a = i(a),
                        e.createRadialChart = function (t) {
                            var e = n(n({}, r.commonChartOptions), {
                                series: [t.percentage],
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
                                            background: r.contextBgColor
                                        },
                                        dataLabels: {
                                            name: {
                                                show: !1
                                            },
                                            value: {
                                                color: "#fff",
                                                fontSize: "20px",
                                                show: !0,
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
                            })
                                , o = new a.default(document.querySelector(t.id), e);
                            o.render(),
                                setTimeout((function () {
                                    o.updateOptions({
                                        chart: {
                                            width: "100%"
                                        }
                                    })
                                }
                                ), 400)
                        }
                }
                    .apply(e, r),
                void 0 === o || (t.exports = o)
        },
        734: function (t, e, a) {
            var r, o, n = this && this.__assign || function () {
                return n = Object.assign || function (t) {
                    for (var e, a = 1, r = arguments.length; a < r; a++)
                        for (var o in e = arguments[a])
                            Object.prototype.hasOwnProperty.call(e, o) && (t[o] = e[o]);
                    return t
                }
                    ,
                    n.apply(this, arguments)
            }
                , i = this && this.__importDefault || function (t) {
                    return t && t.__esModule ? t : {
                        default: t
                    }
                }
                ;
            r = [a, e, a(161), a(549)],
                o = function (t, e, a, r) {
                    "use strict";
                    Object.defineProperty(e, "__esModule", {
                        value: !0
                    }),
                        e.createSimpleAreaChart = void 0,
                        a = i(a),
                        e.createSimpleAreaChart = function (t) {
                            var e = n(n({}, r.commonChartOptions), {
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
                                        enabled: !1
                                    },
                                    toolbar: {
                                        show: !1
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
                                    enabled: !1
                                },
                                yaxis: {
                                    show: !1,
                                    min: 0,
                                    max: 20
                                },
                                xaxis: {
                                    labels: {
                                        show: !1
                                    },
                                    offsetY: 50
                                }
                            })
                                , o = new a.default(document.querySelector(t.id), e);
                            o.render(),
                                setTimeout((function () {
                                    o.updateOptions({
                                        chart: {
                                            width: "100%"
                                        }
                                    })
                                }
                                ), 400)
                        }
                }
                    .apply(e, r),
                void 0 === o || (t.exports = o)
        },
        161: t => {
            "use strict";
            t.exports = ApexCharts
        }
    }
        , e = {}
        , a = function a(r) {
            var o = e[r];
            if (void 0 !== o)
                return o.exports;
            var n = e[r] = {
                exports: {}
            };
            return t[r].call(n.exports, n, n.exports, a),
                n.exports
        }(359);
    leptonxDemoDashboard = a
}
)();
