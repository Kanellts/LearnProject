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
const studentsGrid = document.querySelector("#studentsGrid");
const studentName = document.querySelector("#name");
const studentAge = document.querySelector("#age");
const saveButton = document.querySelector("#btnSave");
const gridOptions = {
    // Grid Options: Contains all of the Data Grid configurations
    columnDefs: [
        { headerName: 'Id', field: 'id', sortable: true, filter: true, editable: false },
        { headerName: 'Name', field: 'name', sortable: true, filter: true, editable: true, cellStyle: { cursor: "pointer" } },
        { headerName: 'Age', field: 'age', sortable: true, filter: true, editable: true, cellStyle: { cursor: "pointer" } },
        { headerName: 'Actions', sortable: false,
            cellRenderer: (params) => {
                const deleteButton = document.createElement("button");
                deleteButton.innerText = "Delete";
                deleteButton.style.backgroundColor = "red";
                deleteButton.style.color = "white";
                deleteButton.style.border = "none";
                deleteButton.style.padding = "5px 10px";
                deleteButton.style.cursor = "pointer";
                deleteButton.style.borderRadius = "5px";
                deleteButton.addEventListener("click", () => deleteStudent(params.data.id));
                return deleteButton;
            }
        }
    ],
    rowData: null,
    onCellEditingStopped: (event) => updateStudent(event.data)
};
const gridApi = agGrid.createGrid(studentsGrid, gridOptions);
// Get students from database
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
//Add a new student
function createStudent(name, age) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            yield axios.post('https://localhost:7103/api/Students', { name, age }, { headers: { "content-type": "application/json" } });
            // clearForm();
            getAllStudents();
        }
        catch (error) {
            console.error("Error creating student: ", error);
        }
    });
}
//Update an existing student
function updateStudent(student) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            yield axios.put(`https://localhost:7103/api/Students/${student.id}`, student, { headers: { "content-type": "application/json" } });
            getAllStudents();
        }
        catch (error) {
            console.error("Error updating student: ", error);
        }
    });
}
//Delete an existing student
function deleteStudent(id) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            yield axios.delete(`https://localhost:7103/api/Students/${id}`);
            getAllStudents();
        }
        catch (error) {
            console.error("Error deleting student: ", error);
        }
    });
}
//Save Button 
saveButton.addEventListener('click', () => {
    createStudent(studentName.value, Number(studentAge.value));
});
getAllStudents();
