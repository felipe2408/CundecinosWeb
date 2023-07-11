$(document).ready(function () {
	graficaPublicaciones();
});
function graficaPublicaciones() {
	$.ajax({
		url: '/APIReport/PublicationReport',
		method: 'GET',
		success: function (data) {
			const dataaux = data
			var counts = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
			data.forEach(x => {
				var objetoFecha = new Date(x.PublicationDate);
				var mes = objetoFecha.getMonth();
				counts[mes]++;
			});
			var options = {
				series: [{
					name: 'publicaciones',
					data: counts
				}],
				chart: {
					type: 'bar',
					height: 350
				},
				plotOptions: {
					bar: {
						horizontal: false,
						columnWidth: '40%',
						borderRadius: 6
					},
				},
				dataLabels: {
					enabled: false
				},
				stroke: {
					show: true,
					width: 2,
					colors: ['transparent']
				},
				xaxis: {
					categories: ["Ene", 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', "Nov", "Dic"],
				},
				yaxis: {
					title: {
						text: 'Cantidad de publicaciones'
					}
				},
				fill: {
					opacity: 1
				},
				tooltip: {
					y: {
						formatter: function (val) {
							return "" + val + " publicaciones"
						}
					}
				}
			};

			var chart = new ApexCharts(document.querySelector("#chart_1"), options);
			chart.render();

			$.ajax({
				url: '/APIReport/CollegeCareerReport',
				method: 'GET',
				success: function (datos) {
					var contador = {};
					datos.forEach(function (x) {
						data.forEach(function (y) {
							if (x.Name === y.Person.CollegeCareer.Name) {
								contador[y.Person.CollegeCareer.Name] = contador[y.Person.CollegeCareer.Name] === undefined || isNaN(contador[y.Person.CollegeCareer.Name]) ? 1 : contador[y.Person.CollegeCareer.Name] + 1;
							}
						});
					});
					const claves = Object.keys(contador);
					const valores = [];

					claves.forEach(clave => {
						valores.push(contador[clave]);
					});
					var options = {
						series: valores,
						chart: {
							type: 'pie',
							height: 350
						},
						labels: claves,
						tooltip: {
							y: {
								formatter: function (val) {
									return "" + val + " publicaciones"
								}
							}
						}
					};

					var chart = new ApexCharts(document.querySelector("#chart_2"), options);
					chart.render();

					$.ajax({
						url: '/APIReport/ExtensionReport',
						method: 'GET',
						success: function (datos) {
							var arreglo = []
							datos.forEach(function (x) {
								var contador = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
								data.forEach(y => {
									var objetoFecha = new Date(y.PublicationDate);
									var mes = objetoFecha.getMonth();
									if (x.Name === y.Person.Extension.Name) {
										contador[mes]++;
									}
								});
								arreglo.push({ name: x.Name, data: contador })
							});
							var options = {
								series: arreglo,
								chart: {
									type: 'area',
									height: 350
								},
								plotOptions: {
									bar: {
										horizontal: false,
										columnWidth: '55%',
										endingShape: 'rounded'
									},
								},
								dataLabels: {
									enabled: false
								},
								stroke: {
									show: true,
									width: 2,
									colors: ['transparent']
								},
								xaxis: {
									categories: ["Ene", 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', "Nov", "Dic"],
								},
								yaxis: {
									title: {
										text: 'Cantidad de publicaciones'
									}
								},
								fill: {
									type: "solid", opacity: 1
								},

								tooltip: {
									y: {
										formatter: function (val) {
											return "" + val + " publicaciones"
										}
									}
								}
							};

							var chart = new ApexCharts(document.querySelector("#chart_3"), options);
							chart.render();
						},
						error: function () {
							console.log('error');
						}
					});

				},
				error: function () {
					console.log('error');
				}
			});

			var publicationStatusCount = [0, 0]
			data.forEach(x => {
				var objetoFecha = new Date(x.PublicationDate);
				var mes = objetoFecha.getMonth();
				if (x.Status === 10) {
					publicationStatusCount[0]++
				}
				if (x.Status === 30) {
					publicationStatusCount[1]++
				}
			});
			var optionsChart = {
				series: publicationStatusCount,
				chart: {
					type: 'pie',
					height: 350
				},
				labels: ['En oferta', 'Intercambiado'],
				tooltip: {
					y: {
						formatter: function (val) {
							return "" + val + " publicaciones"
						}
					}
				}
			};

			var chartCircle = new ApexCharts(document.querySelector("#chart_circle"), optionsChart);
			chartCircle.render();

			var countsNegotiate = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
			var countsExchanged = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
			data.forEach(x => {
				var objetoFecha = new Date(x.PublicationDate);
				var mes = objetoFecha.getMonth();
				if (x.Status === 10) {
					countsNegotiate[mes]++
				}
				if (x.Status === 30) {
					countsExchanged[mes]++
				}
			})
			const arregloLineas = []
			arregloLineas.push({ name: 'En oferta', data: countsNegotiate })
			arregloLineas.push({ name: 'Intercambiado', data: countsExchanged })
			var optionsLines = {
				series: arregloLineas,
				chart: {
					type: 'area',
					height: 350
				},
				plotOptions: {
					bar: {
						horizontal: false,
						columnWidth: '55%',
						endingShape: 'rounded'
					},
				},
				dataLabels: {
					enabled: false
				},
				stroke: {
					show: true,
					width: 2,
					colors: ['transparent']
				},
				xaxis: {
					categories: ["Ene", 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', "Nov", "Dic"],
				},
				yaxis: {
					title: {
						text: 'Cantidad de publicaciones'
					}
				},
				fill: {
					type: "solid", opacity: 1
				},

				tooltip: {
					y: {
						formatter: function (val) {
							return "" + val + " publicaciones"
						}
					}
				}
			}
			var chartLines = new ApexCharts(document.querySelector("#chart_lines"), optionsLines);
			chartLines.render();
			reportoffers()
		},
		error: function () {
			console.log('error');
		}
	});

}

function reportoffers(e) {
	var extensionId = null
	if (e !== undefined) {
		extensionId = e.value
	}
	$.ajax({
		url: '/APIReport/GetOffers' + '?ext=' + extensionId,
		method: 'GET',
		success: function (datos) {
			var countsNegociacion = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
			var countsConcluida = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
			datos.forEach(x => {
				var objetoFecha = new Date(x.CommentDate);
				var mes = objetoFecha.getMonth();
				if (x.StatusInnofer === 10) {
					countsNegociacion[mes]++
				}
				if (x.StatusInnofer === 20) {
					countsConcluida[mes]++
				}
			})
			const arreglo = []
			arreglo.push({ name: 'En Negociacion', data: countsNegociacion })
			arreglo.push({ name: 'Concluida', data: countsConcluida })
			console.log(arreglo)
			var options = {
				series: arreglo,
				chart: {
					height: 350,
					type: 'line',
					dropShadow: {
						enabled: true,
						color: '#000',
						top: 18,
						left: 7,
						blur: 10,
						opacity: 0.2
					},
					toolbar: {
						show: false
					}
				},
				colors: ['#77B6EA', '#545454'],
				dataLabels: {
					enabled: true,
				},
				stroke: {
					curve: 'smooth'
				},
				grid: {
					borderColor: '#e7e7e7',
					row: {
						colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
						opacity: 0.5
					},
				},
				markers: {
					size: 1
				},
				xaxis: {
					categories: ["Ene", 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', "Nov", "Dic"]
				},
				yaxis: {
					title: {
						text: 'Publicaciones'
					}
				},
				legend: {
					position: 'top',
					horizontalAlign: 'right',
					floating: true,
					offsetY: -25,
					offsetX: -5
				}
			};
			$("#offerschart").html('')
			var chart = new ApexCharts(document.querySelector("#offerschart"), options)
			chart.render();
		}
	})

}