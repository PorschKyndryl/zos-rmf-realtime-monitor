# 🖥️ RMFExtrator

RMFExtrator is a Windows Forms application developed in **VB.NET (.NET 8)** designed to extract, parse, and visualize performance data from IBM z/OS RMF console captures.  
It automates the process of reading **raw console output (TXT)**, converting it into structured data, displaying it in advanced grids, and plotting it using **ScottPlot**.

---

## 🚀 Main Features

| Feature | Description |
|----------|-------------|
| **EHLLAPI Integration** | Connects to IBM Personal Communications sessions (3270) to capture console data. |
| **Automatic Extraction** | Reads the terminal screen buffer and saves it into `Buffer.txt` for later analysis. |
| **Real-Time Capture Mode** | Monitors live console updates and continuously extracts new lines at defined intervals. |
| **Data Cleaning & Processing** | Parses captured data lines into structured tables with row/column mapping. |
| **Advanced Filtering** | Uses `DG.AdvancedDataGridView` to support dynamic sorting and filtering. |
| **Chart Visualization** | Plots extracted metrics using `ScottPlot.WinForms` (v5.1.57). |
| **Fixed & Dynamic Indicators** | Adds horizontal lines (max limits) and shaded regions (bands) on plots. |
| **Multi-Tab UI** | Supports both manual and automatic extraction modes. |

---

## 🧩 Project Structure

RMFExtrator/
├── EHLLAPI.vb # Wrapper for IBM EHLLAPI communication
├── frm_pcomm.vb # Main terminal capture interface
├── frm_pcomm.Designer.vb # Windows Forms layout (capture form)
├── Form1.vb # Main dashboard (data parsing and plotting)
├── My Project/ # Auto-generated project resources
├── EHLAPI32.dll, pcshll32.dll # Required IBM PCOMM libraries
└── Buffer.txt # Generated output file after capture

---

## ⚙️ How It Works

### 1. **Data Capture**

- The app connects to a **Personal Communications (PCOMM)** session using EHLLAPI.
- It reads the screen buffer (24x80 grid) and stores the raw data into `Buffer.txt`.
- You can capture manually (`Manual` tab) or enable real-time mode (`Automatic` tab).

### 2. **Data Processing**

- The main form (`Form1`) reads `Buffer.txt`.
- Each line is mapped to a `DataTable` and displayed in `dg_result`.
- Duplicate removal and sorting are handled through the **AdvancedDataGridView** component.

### 3. **Visualization**

- The data can be plotted using **ScottPlot** with:
  - `Y Linha Fixa (Max)` → Dashed line at maximum value
  - `X Sombra` → Shaded region (band) for highlighted ranges
- Plots are interactive (zoom, pan, export to image).

### 4. **Export**

- Cleaned or filtered data can be exported to **CSV** or **Excel** format.
- The chart can be saved as **PNG**.

---

## 🪄 Controls Overview

| Control | Purpose |
|----------|----------|
| `btn_exec` | Parses and loads data into the grid. |
| `btn_limpar` | Clears the loaded buffer. |
| `btn_plot` | Generates the ScottPlot chart. |
| `CheckBox1` | Enables real-time capture. |
| `NumericUpDown` | Defines capture range or line limits. |
| `ToolStripComboBox1` | Selects the PCOMM session (A, B, C, D). |

---

## 📊 Chart Features

- **Legend**: Automatically displays each metric name.
- **Max Line**: Dashed line for “Y Linha Fixa (Max)” fields.
- **Shaded Bands**: Highlighted zones for “X Sombra”.
- **Dark Mode** theme with customizable colors.

---

## 🧱 Dependencies

| Package | Version | Purpose |
|----------|----------|----------|
| `DG.AdvancedDataGridView` | 1.2.30115.18 | Interactive sorting/filtering grid |
| `ScottPlot.WinForms` | 5.1.57 | Chart plotting library |
| `OpenTK` | 3.1.0 (excluded) | Internal ScottPlot dependency |
| `SkiaSharp.Views.WindowsForms` | 3.119.0 | Graphics backend for ScottPlot |
| `Microsoft.Office.Interop.Excel` | GAC | Optional Excel export |

---

## 🪛 Requirements

- **Windows 10/11**
- **.NET 8 SDK or Runtime**
- **IBM Personal Communications (PCOMM)** installed and configured
- `EHLAPI32.dll` and `pcshll32.dll` in the same directory as the executable

---

## 🧠 Notes

- Ensure your EHLLAPI session (A–D) matches the one configured in PCOMM.
- `Buffer.txt` is automatically updated after each extraction.
- When real-time mode is enabled, data refreshes every few seconds based on the range value.

---

## 🧾 Example Workflow

1. Open **RMFExtrator**.
2. Select session `A` from the toolbar.
3. Click **Capturar Tela** to extract the current console.
4. Switch to the **Main** tab to process and plot the data.
5. Click **Plot** to generate a chart.

---

## 📂 Output Example


- The program reads and formats this into:
  - Date range
  - Metric values
  - Page boundaries

---

## 🧑‍💻 Developer Notes

- Code uses **Threading** for non-blocking real-time updates.
- All UI updates are marshaled to the main thread via `Invoke`.
- Functions are written in English for maintainability and future contributions.
- TryParse and culture handling are implemented for numeric conversions.

---

## 🧰 Build Instructions

1. Open the solution in **Visual Studio 2022**.
2. Select **Release / Any CPU**.
3. Build using the **.NET Framework MSBuild** (required for COM references).
   ```bash
   msbuild RMFExtrator.vbproj /p:Configuration=Release