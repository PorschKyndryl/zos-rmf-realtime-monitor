# 🧩 RMF Extractor – PCM Tool for IBM z/OS RMF Hardcopy Files

**RMF Extractor** is a Windows desktop application written in **VB.NET (.NET Framework)** that parses and visualizes IBM z/OS **RMF (Resource Measurement Facility)** text reports in real time or RMFPP file.  
It allows analysts to extract metrics, remove duplicates, and create charts from raw RMF hardcopy files, enabling near real-time visualization of CPU, memory, and workload metrics from mainframe systems.

---

## 🚀 Features

- **Structured extraction** of data from RMF text reports using line/column definitions  
- Supports both **Fixed Header** and **Table Region** parsing modes  
- **Automatic charting** with [ScottPlot](https://scottplot.net/) (CPU, MSU, workload, etc.)  
- **Duplicate row detection and cleanup**  
- **Excel export** via `Microsoft.Office.Interop.Excel`  
- Multiple pre-configured templates:
  - `CPC` (CPU/MSU utilization)
  - `PROCU` (Processor utilization)
  - `Channel`
  - `OMVS`
  - `SysSum` (System Summary)
- **Dark Mode** visualization  
- **Optional PCOMM capture integration** (to import RMF console buffers directly)

---

## 🧠 Architecture Overview

```
┌─────────────────────────────┐
│      RMF Extractor GUI      │
│  (Windows Forms Application)│
└──────────────┬──────────────┘
               │
               ▼
┌──────────────────────────────┐
│ RMF Text File (.txt) Input   │
│ - CPC, SYSUM, OMVS, etc.     │
└──────────────────────────────┘
               │
               ▼
┌──────────────────────────────┐
│ Parser Engine (Importar)     │
│ - Reads line/column mappings │
│ - Validates data types       │
│ - Removes empty/invalid rows │
└──────────────────────────────┘
               │
               ▼
┌──────────────────────────────┐
│ BindingSource + DataGridView │
│ - Displays structured data   │
│ - Allows sorting/filtering   │
└──────────────────────────────┘
               │
               ▼
┌──────────────────────────────┐
│ Chart Engine (ScottPlot)     │
│ - Supports X/Y or categorized│
│   series with time/date axes │
└──────────────────────────────┘
               │
               ▼
┌──────────────────────────────┐
│ Export Engine (Excel/CSV)    │
└──────────────────────────────┘
```

---

## 🛠️ Technologies Used

| Component | Purpose |
|------------|----------|
| **VB.NET / .NET Framework** | Main UI and logic |
| **ScottPlot.WinForms** | Real-time chart rendering |
| **Zuby.ADGV** | Advanced DataGridView (sorting & filtering) |
| **Microsoft.Office.Interop.Excel** | Excel export integration |
| **OpenTK.Graphics.ES30** | Graphics dependencies |
| **BackgroundWorker** | Async imports/exports |

---

## 🧩 Main Components

| Class | Description |
|-------|--------------|
| `Form1.vb` | Main UI logic and data-processing engine |
| `frm_pcomm.vb` | Optional PCOMM capture window |
| `Importar()` | Core parser function |
| `Plotar()` | Chart creation and rendering |
| `Exportar()` | Excel export logic |
| `RemoverDuplicados()` | Duplicate cleanup routine |
| `ApplyScottPlotDarkMode()` | Dark theme styling |

---

## ⚙️ Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/porsch91/zos-rmf-realtime-monitor.git
   cd zos-rmf-realtime-monitor
   ```

2. Open the project in **Visual Studio (2022 or later)**.  
   Target framework: **.NET Framework 4.8**

3. Install NuGet dependencies:
   - `ScottPlot.WinForms`
   - `Zuby.ADGV`
   - `Microsoft.Office.Interop.Excel`

4. Build and run the project (`Ctrl + F5`).

---

## 🧪 Usage

1. Select an **RMF hardcopy file** (e.g. `RMF CPC PROD1.txt`)  
2. Choose a **template** (CPC, PROCU, Channel, etc.) or make yours
3. Click **Run (TXT icon)** to parse the file
4. Filter, sort, and explore the structured data in the **Structured Data** tab
5. Switch to the **Chart** tab to visualize CPU/MSU utilization, workload, or performance trends
6. Export results to Excel if desired

---

## 🧰 Common Error Fixes

| Error | Cause | Fix |
|-------|--------|-----|
| `Excel Interop COMException` | Excel not installed | Install Microsoft Office or use CSV export alternative |

---

## 🕹️ Keyboard Shortcuts

| Action | Shortcut |
|---------|-----------|
| Run extraction | **Ctrl + E** |
| Open file | **Ctrl + O** |
| Export to Excel | **Ctrl + X** |
| Plot chart | **Ctrl + P** |
| Remove duplicates | **Ctrl + D** |

---

## 📸 Example

![RMF Extractor video](https://www.linkedin.com/posts/matheus-porsch-22b29a220_generating-real-time-graphs-with-data-directly-activity-7246248241216864256-yxq9?utm_source=share&utm_medium=member_desktop&rcm=ACoAADeSNcMBFpMGjR5jdSuPwxVyY8qaHYPuOHk)

*(Example view: See an example in the video where I collect usage information (MSU) from LPARs on the RMF CPC (Central Processor Complex) Capacity screen. The graph is generated with real-time data available in a standard RMF range of 100 seconds)*

---

## 🧑‍💻 Author

**Matheus Porsch**  
![LinkedIn](https://www.linkedin.com/in/matheus-porsch-22b29a220/)
Systems Administration - Kyndryl Global Services Engineering / Kyndryl  
Developed as an open-source utility for RMF data analysis and visualization.

---

## 🪪 License

This project is licensed under the **MIT License** — see the [LICENSE](LICENSE) file for details.

---

## 🌐 Links

- [ScottPlot Documentation](https://scottplot.net/)
- [IBM RMF Reference](https://www.ibm.com/docs/en/zos/latest?topic=facility-resource-measurement)
- [Zuby.ADGV GitHub](https://github.com/marcelmue/Zuby.ADGV)
