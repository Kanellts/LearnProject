const studentsGrid = document.querySelector<HTMLDivElement>("#studentsGrid");
const studentName = document.querySelector<HTMLInputElement>("#name");
const studentAge = document.querySelector<HTMLInputElement>("#age");
const saveButton = document.querySelector<HTMLButtonElement>("#btnSave");

declare const agGrid : any;

interface Student {
    id: number;
    name: string;
    age: number;
}

interface StudentDTO {
    name: string;
    age: number;
}


const gridOptions = {
    // Grid Options: Contains all of the Data Grid configurations
    columnDefs: [
        { headerName: 'Id', field: 'id', sortable: true, filter: true, editable: false},
        { headerName: 'Name', field: 'name', sortable: true, filter: true, editable: true, cellStyle: { cursor: "pointer" } },
        { headerName: 'Age', field: 'age', sortable: true, filter: true, editable: true, cellStyle: { cursor: "pointer"} },
        { headerName: 'Actions', sortable: false,
            cellRenderer: (params : any) => {
                const deleteButton = document.createElement("button");
                deleteButton.innerText = "Delete";
                deleteButton.style.backgroundColor = "red";  
                deleteButton.style.color = "white";          
                deleteButton.style.border = "none";           
                deleteButton.style.padding = "5px 10px";     
                deleteButton.style.cursor = "pointer";        
                deleteButton.style.borderRadius = "5px"
                deleteButton.addEventListener("click", () => deleteStudent(params.data.id));
                return deleteButton;
            }
        }
    ],
    rowData: null,
    onCellEditingStopped: (event : any) => updateStudent(event.data)
};

const gridApi = agGrid.createGrid(studentsGrid, gridOptions);

// Get students from database
async function getAllStudents() {
    try {
        const response = await axios.get<Student[]>('https://localhost:7103/api/Students');
        gridApi.setGridOption('rowData', response.data);
    } 
    catch(error) {
        console.error("Error fetching students: ", error);
    }
}

//Add a new student
async function createStudent(name: string, age: number) {
    try {
        await axios.post<StudentDTO>('https://localhost:7103/api/Students', { name, age }, {headers: {"content-type": "application/json"}});
        // clearForm();
        getAllStudents();
    }
    catch(error) {
        console.error("Error creating student: ", error);
    }
}

//Update an existing student
async function updateStudent(student : Student) {
    try {
        await axios.put<StudentDTO>(`https://localhost:7103/api/Students/${student.id}`, student, { headers: {"content-type": "application/json"} });
        getAllStudents();
    }
    catch(error) {
        console.error("Error updating student: ", error);
    }
}

//Delete an existing student

async function deleteStudent(id: number) {
    try {
        await axios.delete(`https://localhost:7103/api/Students/${id}`);
        getAllStudents();
    }
    catch(error) {
        console.error("Error deleting student: ", error);
    }
}

//Save Button 
saveButton!.addEventListener('click', () => {
    createStudent(studentName!.value, Number(studentAge!.value));
});

getAllStudents();