@page
@model AttendancesModel
@{
    ViewData["Title"] = "Mark Attendance";
}

<div class="text-center mb-4">
    <h1 class="display-4">Mark Attendance</h1>
    <p class="lead">Date: @DateTime.Now.ToString("MMMM dd, yyyy")</p>
</div>

@if (Model.Students.Any())
{
    <form method="post">
        <div class="card" style="border: 1px solid #dee2e6;">
            <div class="card-header d-flex justify-content-between align-items-center" style="background-color: white; border-bottom: 1px solid #dee2e6;">
                <h3 style="font-weight: normal;">Student List</h3>
                <div>
                    <button type="submit" class="btn" style="background-color: white; border: 1px solid #dee2e6; color: black;">Save Attendance</button>
                </div>
            </div>
            <div class="card-body" style="background-color: white;">
                <div class="table-responsive">
                    <table class="table" style="background-color: white;">
                        <thead style="background-color: white;">
                            <tr>
                                <th style="font-weight: normal;">Student ID</th>
                                <th style="font-weight: normal;">Full Name</th>
                                <th style="font-weight: normal;">Section</th>
                                <th style="font-weight: normal;">Present</th>
                                <th style="font-weight: normal;">Absent</th>
                            </tr>
                        </thead>
                        <tbody style="background-color: white;">
                            @for (int i = 0; i < Model.Students.Count; i++)
                            {
                                <tr style="background-color: white;">
                                    <td>
                                        @Model.Students[i].StudentId
                                        <input type="hidden" asp-for="Students[i].StudentId" />
                                        <input type="hidden" asp-for="Students[i].FullName" />
                                        <input type="hidden" asp-for="Students[i].Section" />
                                    </td>
                                    <td>@Model.Students[i].FullName</td>
                                    <td>@Model.Students[i].Section</td>
                                    <td>
                                        <input type="radio" asp-for="AttendanceRecords[i].Status" value="Present"
                                               class="form-check-input" id="present_@i" />
                                        <label class="form-check-label" for="present_@i" style="color: black;">Present</label>
                                    </td>
                                    <td>
                                        <input type="radio" asp-for="AttendanceRecords[i].Status" value="Absent"
                                               class="form-check-input" id="absent_@i" />
                                        <label class="form-check-label" for="absent_@i" style="color: black;">Absent</label>
                                    </td>
                                </tr>
                            }
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </form>

    @if (TempData["AttendanceMessage"] != null)
    {
        <div class="alert mt-3" role="alert" style="background-color: white; border: 1px solid #dee2e6; color: black;">
            @TempData["AttendanceMessage"]
        </div>
    }
}
else
{
    <div class="alert" role="alert" style="background-color: white; border: 1px solid #dee2e6; color: black;">
        <h4>No Students Found</h4>
        <a href="/" class="btn" style="background-color: white; border: 1px solid #dee2e6; color: black;">Add Students</a>
    </div>
}
