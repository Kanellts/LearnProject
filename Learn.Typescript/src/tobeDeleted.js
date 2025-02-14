"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};

// Grid Options
const gridOptions = {
    columnDefs: [
        { headerName: 'Id', field: 'id', sortable: true, filter: true },
        { headerName: 'Name', field: 'name', sortable: true, filter: true, editable: true },
        { headerName: 'Age', field: 'age', sortable: true, filter: true, editable: true },
        {
            headerName: "Actions",
            cellRenderer: (params) => {
                const btn = document.createElement("button");
                btn.innerText = "Delete";
                btn.addEventListener("click", () => deleteStudent(params.data.id));
                return btn;
            }
        }
    ],
    rowData: null,
    onCellEditingStopped: (event) => updateStudent(event.data)
};

const studentsGrid = document.querySelector("#studentsGrid");
const gridApi = agGrid.createGrid(studentsGrid, gridOptions);

// Fetch all students
function getAllStudents() {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const response = yield axios.get('https://localhost:7103/api/Students');
            gridApi.setGridOption('rowData', response.data);
        }
        catch (error) {
            console.error("Error fetching students: ", error);
        }
    });
}

// Create a new student
function createStudent(name, age) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const newStudent = { name, age };
            yield axios.post('https://localhost:7103/api/Students', newStudent);
            getAllStudents(); // Refresh grid
        }
        catch (error) {
            console.error("Error adding student: ", error);
        }
    });
}

// Update a student
function updateStudent(updatedStudent) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            yield axios.put(`https://localhost:7103/api/Students/${updatedStudent.id}`, updatedStudent);
            getAllStudents(); // Refresh grid
        }
        catch (error) {
            console.error("Error updating student: ", error);
        }
    });
}

// Delete a student
function deleteStudent(id) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            yield axios.delete(`https://localhost:7103/api/Students/${id}`);
            getAllStudents(); // Refresh grid
        }
        catch (error) {
            console.error("Error deleting student: ", error);
        }
    });
}

// Add button event listener to create a student
document.querySelector("#addStudentBtn").addEventListener("click", () => {
    const name = document.querySelector("#studentName").value;
    const age = document.querySelector("#studentAge").value;
    if (name && age) {
        createStudent(name, parseInt(age));
    }
});

// Load students when page loads
getAllStudents();
