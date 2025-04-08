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
#SA_i100_t01_meanValues_exp = [1186, 1150, 1090, 1007, 976]
#SA_i100_t01_bestValues_exp = [1079, 971, 937, 870, 867]
#SA_i100_t01_worstValues_exp = [1312, 1252, 1226, 1180, 1151]
SA_i100_t01_runtimes_exp = [4, 4, 6, 47, 203]

#SA_i100_t01_meanValues_lin = [1131, 1157, 1342, 1342, 1299]
#SA_i100_t01_bestValues_lin = [1021, 1086, 1205, 1249, 1100]
#SA_i100_t01_worstValues_lin = [1262, 1243, 1439, 1483, 1440]
SA_i100_t01_runtimes_lin = [5, 3, 1.5, 2.5, 1]

#plt.xlabel("Alpha value")
#plt.ylabel("Fitness score")
#plt.title("100 Iterations SAA, T0 = 1, T_min = 0.0001, Exponential T")

#plt.plot(SA_AlphaValues, SA_i100_t01_meanValues_exp, label = "Mean")
#plt.plot(SA_AlphaValues, SA_i100_t01_bestValues_exp, label = "Best")
#plt.plot(SA_AlphaValues, SA_i100_t01_worstValues_exp, label = "Worst")

#plt.legend()

#plt.show()

#plt.xlabel("Alpha value")
#plt.ylabel("Fitness score")
#plt.title("100 Iterations SAA, T0 = 1, T_min = 0.0001, Linear T")

#plt.plot(SA_AlphaValues, SA_i100_t01_meanValues_lin, label = "Mean")
#plt.plot(SA_AlphaValues, SA_i100_t01_bestValues_lin, label = "Best")
#plt.plot(SA_AlphaValues, SA_i100_t01_worstValues_lin, label = "Worst")

#plt.legend()
#plt.show()

# Gen = 100, runtimes
#plt.plot(gen100_popsizes, gen100_runtimes)
#plt.xlabel("Population size")
#plt.ylabel("Runtime [ms]")
#plt.title("100 Generations GA, CC - 99%, MC - 5%")
# Gen = 100, runtimes - finish

# SA runtimes, Exp/Lin
#plt.plot(SA_AlphaValues, SA_i100_t01_runtimes_exp, label = "Exponential T change")
#plt.plot(SA_AlphaValues, SA_i100_t01_runtimes_lin, label = "Linear T change")
#plt.xlabel("Alpha value")
#plt.ylabel("Runtime [ms]")
#plt.title("Exponential vs Linear T change")
#plt.legend()
#plt.show()
# SA runtimes, Exp/Lin - finish


# best instances
instances = ["An32k5", "An37k6", "An39k5", "An45k6", "An48k7", "An54k7", "An60k9"]
best_GA = [802, 968, 839, 981, 1140, 1286, 1479]
best_RSA = [1321, 1515, 1429, 1994, 2033, 2346, 2760]
best_SAA = [864, 1006, 934, 1093, 1197, 1350, 1591]
best_GrA = [1139, 1372, 1098, 1485, 1405, 1439, 2042]
optimal_v = [784, 949, 822, 944, 1073, 1167, 1354]
plt.plot(instances, best_GA, label = "Genetic Algorithm")
plt.plot(instances, best_RSA, label = "Random Search Algorithm")
plt.plot(instances, best_SAA, label = "Simulated Annealing Algorithm")
plt.plot(instances, best_GrA, label = "Greedy Algorithm")
plt.plot(instances, optimal_v, label = "Optimal")
#
plt.xlabel("Instances")
plt.ylabel("Fitness score")
plt.title("Best fitness per instance")
#
plt.legend()
plt.show()
# best instances - finish