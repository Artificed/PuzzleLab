window.charts = window.charts || {};

window.chartInterop = {
    renderPerformanceChart: function (elementId, jsonData) {
        try {
            const el = document.getElementById(elementId);
            if (!el) {
                console.error("ChartJS: Canvas element not found:", elementId);
                return;
            }
            const ctx = el.getContext("2d");
            if (!ctx) {
                console.error("ChartJS: Could not get 2D context for:", elementId);
                return;
            }
            const data = JSON.parse(jsonData);


            if (!Array.isArray(data)) {
                console.error("ChartJS: Invalid data format received for performance chart. Expected array.", data);
                return;
            }


            if (window.charts[elementId]) {
                window.charts[elementId].destroy();
                console.log("ChartJS: Destroyed previous chart instance for:", elementId);
            }

            console.log("ChartJS: Rendering performance chart on", elementId, "with data:", data);


            window.charts[elementId] = new Chart(ctx, {
                type: "line",
                data: {

                    labels: data.map(d => d.date),
                    datasets: [{
                        label: "Score (%)",
                        data: data.map(d => d.score),
                        backgroundColor: "rgba(54, 162, 235, 0.2)",
                        borderColor: "rgba(54, 162, 235, 1)",
                        borderWidth: 2,
                        pointRadius: 4,
                        tension: 0.4
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    scales: {
                        y: {
                            beginAtZero: true,
                            max: 100,
                            title: {display: true, text: "Score (%)"}
                        },
                        x: {
                            title: {display: true, text: "Date"}

                        }
                    },
                    plugins: {
                        tooltip: {
                            callbacks: {

                                title: function (tooltipItems) {

                                    const index = tooltipItems[0]?.dataIndex;
                                    return (index !== undefined && data[index]?.quiz) ? data[index].quiz : '';
                                },
                                label: function (context) {

                                    let label = context.dataset.label || '';
                                    if (label) {
                                        label += ': ';
                                    }
                                    if (context.parsed.y !== null) {
                                        label += context.parsed.y.toFixed(1) + '%';
                                    }
                                    return label;
                                }
                            }
                        },
                        legend: {
                            display: true
                        }
                    }
                }
            });
        } catch (e) {
            console.error("ChartJS: Error rendering performance chart:", e);
        }
    },

    renderScoreDistributionChart: function (elementId, jsonData) {
        try {
            const el = document.getElementById(elementId);
            if (!el) {
                console.error("ChartJS: Canvas element not found:", elementId);
                return;
            }
            const ctx = el.getContext("2d");
            if (!ctx) {
                console.error("ChartJS: Could not get 2D context for:", elementId);
                return;
            }
            const data = JSON.parse(jsonData);

            if (!Array.isArray(data) || data.length === 0) {
                console.error("ChartJS: Invalid data format received for score distribution chart.", data);
                return;
            }

            if (window.charts[elementId]) {
                window.charts[elementId].destroy();
                console.log("ChartJS: Destroyed previous chart instance for:", elementId);
            }

            console.log("ChartJS: Rendering score distribution chart on", elementId, "with data:", data);

            window.charts[elementId] = new Chart(ctx, {
                type: "pie",
                data: {
                    labels: data.map(d => d.name),
                    datasets: [{
                        data: data.map(d => d.value),
                        backgroundColor: ["rgba(75, 192, 192, 0.7)", "rgba(255, 99, 132, 0.7)"],
                        borderColor: ["rgba(75, 192, 192, 1)", "rgba(255, 99, 132, 1)"],
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: "bottom"
                        },
                        tooltip: {
                            callbacks: {
                                label: function (context) {
                                    let label = context.label || '';
                                    if (label) {
                                        label += ': ';
                                    }
                                    if (context.parsed !== null) {

                                        const total = context.chart.data.datasets[0].data.reduce((a, b) => a + b, 0);
                                        const percentage = total > 0 ? ((context.parsed / total) * 100).toFixed(1) : 0;
                                        label += `${context.raw} (${percentage}%)`;
                                    }
                                    return label;
                                }
                            }
                        }
                    }
                }
            });
        } catch (e) {
            console.error("ChartJS: Error rendering score distribution chart:", e);
        }
    },

    renderTimeAnalysisChart: function (elementId, jsonData) {
        try {
            const el = document.getElementById(elementId);
            if (!el) {
                console.error("ChartJS: Canvas element not found:", elementId);
                return;
            }
            const ctx = el.getContext("2d");
            if (!ctx) {
                console.error("ChartJS: Could not get 2D context for:", elementId);
                return;
            }
            const data = JSON.parse(jsonData);

            if (!Array.isArray(data) || data.length === 0) {
                console.error("ChartJS: Invalid data format received for time analysis chart.", data);
                return;
            }

            if (window.charts[elementId]) {
                window.charts[elementId].destroy();
                console.log("ChartJS: Destroyed previous chart instance for:", elementId);
            }

            console.log("ChartJS: Rendering time analysis chart on", elementId, "with data:", data);

            window.charts[elementId] = new Chart(ctx, {
                type: "bar",
                data: {
                    labels: data.map(d => d.name),
                    datasets: [{
                        label: "Minutes",
                        data: data.map(d => d.value),
                        backgroundColor: ["rgba(153, 102, 255, 0.7)", "rgba(255, 159, 64, 0.7)"],
                        borderColor: ["rgba(153, 102, 255, 1)", "rgba(255, 159, 64, 1)"],
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    scales: {
                        y: {
                            beginAtZero: true,
                            title: {display: true, text: "Minutes"}
                        }
                    },
                    plugins: {
                        legend: {
                            display: false
                        },
                        tooltip: {
                            callbacks: {
                                label: function (context) {
                                    let label = context.dataset.label || '';
                                    if (label) {
                                        label += ': ';
                                    }
                                    if (context.parsed.y !== null) {
                                        label += context.parsed.y.toFixed(2);
                                    }
                                    return label;
                                }
                            }
                        }
                    }
                }
            });
        } catch (e) {
            console.error("ChartJS: Error rendering time analysis chart:", e);
        }
    }
};
