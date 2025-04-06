import matplotlib.pyplot as plt

#gen100_popsizes = [100, 200, 500, 1000, 2000]
#gen100_meanValues = [1089, 958, 919, 884, 841]
#gen100_bestValues = [994, 839, 851, 807, 802]
#gen100_worstValues = [1181, 1003, 979, 897, 876]
#gen100_runtimes = [124, 176, 299, 461, 893]

#plt.plot(gen100_popsizes, gen100_meanValues, label = "Mean")
#plt.plot(gen100_popsizes, gen100_bestValues, label = "Best")
#plt.plot(gen100_popsizes, gen100_worstValues, label = "Worst")

#plt.xlabel("Population size")
#plt.ylabel("Fitness score")
#plt.title("100 Generations GA, CC - 99%, MC - 5%")

#plt.legend()

#plt.show()

#plt.plot(gen100_popsizes, gen100_runtimes)
#plt.xlabel("Population size")
#plt.ylabel("Runtime [ms]")
#plt.title("100 Generations GA, CC - 99%, MC - 5%")

#plt.show()
#gen100_popsizes = [100, 200, 500, 1000, 2000]
#gen100_meanValues = [1089, 958, 919, 884, 841]
#gen100_bestValues = [994, 839, 851, 807, 802]
#gen100_worstValues = [1181, 1003, 979, 897, 876]
#gen100_runtimes = [124, 176, 299, 461, 893]
#
#plt.plot(gen100_popsizes, gen100_meanValues, label = "Mean")
#plt.plot(gen100_popsizes, gen100_bestValues, label = "Best")
#plt.plot(gen100_popsizes, gen100_worstValues, label = "Worst")

SA_AlphaValues = [0.1, 0.25, 0.5, 0.9, 0.99]
SA_i100_t01_meanValues_exp = [1186, 1150, 1090, 1007, 976]
SA_i100_t01_bestValues_exp = [1079, 971, 937, 870, 867]
SA_i100_t01_worstValues_exp = [1312, 1252, 1226, 1180, 1151]
SA_i100_t01_runtimes_exp = [4, 4, 6, 47, 203]

SA_i100_t01_meanValues_lin = [1131, 1157, 1342, 1342, 1299]
SA_i100_t01_bestValues_lin = [1021, 1086, 1205, 1249, 1100]
SA_i100_t01_worstValues_lin = [1262, 1243, 1439, 1483, 1440]
SA_i100_t01_runtimes_lin = [5, 3, 1.5, 2.5, 1]

plt.xlabel("Aplha value")
plt.ylabel("Fitness score")
plt.title("100 Iterations SAA, T0 = 1, T_min = 0.0001, Exponential T")

plt.plot(SA_AlphaValues, SA_i100_t01_meanValues_exp, label = "Mean")
plt.plot(SA_AlphaValues, SA_i100_t01_bestValues_exp, label = "Best")
plt.plot(SA_AlphaValues, SA_i100_t01_worstValues_exp, label = "Worst")

plt.legend()

plt.show()

plt.xlabel("Aplha value")
plt.ylabel("Fitness score")
plt.title("100 Iterations SAA, T0 = 1, T_min = 0.0001, Linear T")

plt.plot(SA_AlphaValues, SA_i100_t01_meanValues_lin, label = "Mean")
plt.plot(SA_AlphaValues, SA_i100_t01_bestValues_lin, label = "Best")
plt.plot(SA_AlphaValues, SA_i100_t01_worstValues_lin, label = "Worst")

plt.legend()

plt.show()

#plt.plot(gen100_popsizes, gen100_runtimes)
#plt.xlabel("Population size")
#plt.ylabel("Runtime [ms]")
#plt.title("100 Generations GA, CC - 99%, MC - 5%")

plt.plot(SA_AlphaValues, SA_i100_t01_runtimes_exp, label = "Exponential T change")
plt.plot(SA_AlphaValues, SA_i100_t01_runtimes_lin, label = "Linear T change")
plt.xlabel("Population size")
plt.ylabel("Runtime [ms]")
plt.title("100 Generations GA, CC - 99%, MC - 5%")

plt.show()