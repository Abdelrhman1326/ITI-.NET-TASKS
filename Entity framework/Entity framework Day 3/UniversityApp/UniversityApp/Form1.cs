using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using UniversityApp.Data;
using UniversityApp.Models;
using System.Collections.Generic;
using System.Drawing; // For Point structure

namespace UniversityApp
{
    public partial class Form1 : Form
    {
        // Tracks the currently active view (courses or instructors)
        private string active_space = "";

        // Constructor and Initial Setup
        public Form1()
        {
            InitializeComponent();

            // Set the initial state and load the Courses data on startup
            active_space = "courses";
            fetch_courses();

            // Set focus to the Courses button (button1)
            button1.Focus();
        }

        // Helper method to clear input fields
        private void ClearInputs()
        {
            if (Controls.Find("textBox1", true).FirstOrDefault() is TextBox tb1)
                tb1.Text = string.Empty;
            if (Controls.Find("textBox2", true).FirstOrDefault() is TextBox tb2)
                tb2.Text = string.Empty;
            if (Controls.Find("numericUpDown1", true).FirstOrDefault() is NumericUpDown nud)
                nud.Value = nud.Minimum;
            if (Controls.Find("comboBox1", true).FirstOrDefault() is ComboBox cb)
                cb.SelectedIndex = -1;
        }

        // Helper method to manage ComboBox visibility
        private void ToggleComboBoxVisibility(bool isVisible)
        {
            if (Controls.Find("comboBox1", true).FirstOrDefault() is ComboBox cb)
            {
                if (isVisible)
                    cb.Show();
                else
                {
                    cb.Hide();
                    cb.DataSource = null;
                }
            }
        }

        // Helper method to manage TextBox2 visibility
        private void ToggleTextBox2Visibility(bool isVisible)
        {
            if (Controls.Find("textBox2", true).FirstOrDefault() is TextBox tb2)
            {
                if (isVisible)
                    tb2.Show();
                else
                {
                    tb2.Hide();
                    tb2.Text = string.Empty;
                }
            }
        }

        // ===================================================================
        // 1. Course Management Fetch Method (button1)
        // ===================================================================
        private void fetch_courses()
        {
            // Move Add button
            if (Controls.Find("button4", true).FirstOrDefault() is Button btn4)
                btn4.Location = new Point(756, 357);

            // Move Delete button
            if (Controls.Find("button2", true).FirstOrDefault() is Button btn2)
                btn2.Location = new Point(756, 402);

            if (Controls.Find("numericUpDown1", true).FirstOrDefault() is NumericUpDown nud)
                nud.Show();

            ToggleTextBox2Visibility(false);
            ToggleComboBoxVisibility(true);

            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var instructors = context.Instructors
                        .Select(i => new { i.InstructorId, i.Name })
                        .ToList();

                    if (Controls.Find("comboBox1", true).FirstOrDefault() is ComboBox cb)
                    {
                        cb.DataSource = instructors;
                        cb.DisplayMember = "Name";
                        cb.ValueMember = "InstructorId";
                        cb.SelectedIndex = -1;
                    }

                    var courses = context.Courses
                        .Include(c => c.Explainer)
                        .Select(c => new
                        {
                            c.CourseId,
                            c.Title,
                            c.Credits,
                            InstructorName = c.Explainer.Name
                        })
                        .ToList();

                    dataGridView1.DataSource = courses;
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching courses: {ex.Message}", "Database Error",
                                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            label1.Text = "Title";
            label2.Text = "Explainer (Select Instructor)";
            label3.Text = "Credits";
        }

        // ===================================================================
        // 3. Instructor Management Fetch Method (button3)
        // ===================================================================
        private void fetch_instructors()
        {
            // Move Add button
            if (Controls.Find("button4", true).FirstOrDefault() is Button btn4)
                btn4.Location = new Point(756, 187);

            // Move Delete button
            if (Controls.Find("button2", true).FirstOrDefault() is Button btn2)
                btn2.Location = new Point(756, 230);

            if (Controls.Find("numericUpDown1", true).FirstOrDefault() is NumericUpDown nud)
                nud.Hide();

            ToggleTextBox2Visibility(false);
            ToggleComboBoxVisibility(false);

            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var instructors = context.Instructors
                        .Select(i => new
                        {
                            i.InstructorId,
                            i.Name
                        })
                        .ToList();

                    dataGridView1.DataSource = instructors;
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching instructors: {ex.Message}", "Database Error",
                                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            label1.Text = "Name";
            label2.Text = "";
            label3.Text = "";
        }

        // ===================================================================
        // DATA SUBMISSION METHODS
        // ===================================================================

        private void AddCourse()
        {
            var titleBox = Controls.Find("textBox1", true).FirstOrDefault() as TextBox;
            var creditsNud = Controls.Find("numericUpDown1", true).FirstOrDefault() as NumericUpDown;
            var instructorComboBox = Controls.Find("comboBox1", true).FirstOrDefault() as ComboBox;

            if (titleBox == null || creditsNud == null || instructorComboBox == null)
            {
                MessageBox.Show("Input controls are not configured correctly.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(titleBox.Text) || instructorComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please fill in the Course Title and select an Instructor.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!(instructorComboBox.SelectedValue is int instructorId))
            {
                if (!int.TryParse(instructorComboBox.SelectedValue.ToString(), out instructorId))
                {
                    MessageBox.Show("Selected Instructor ID is invalid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            int credits = (int)creditsNud.Value;

            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var newCourse = new Course
                    {
                        Title = titleBox.Text,
                        InstructorId = instructorId,
                        Credits = credits
                    };

                    context.Courses.Add(newCourse);
                    context.SaveChanges();

                    MessageBox.Show("Course added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (DbUpdateException dbEx)
                {
                    MessageBox.Show($"A database error occurred: {dbEx.InnerException?.Message ?? dbEx.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Submission Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            fetch_courses();
            ClearInputs();
        }

        private void AddInstructor()
        {
            var nameBox = Controls.Find("textBox1", true).FirstOrDefault() as TextBox;

            if (nameBox == null)
            {
                MessageBox.Show("Input control (textBox1) is not configured correctly.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(nameBox.Text))
            {
                MessageBox.Show("Please fill in the Instructor Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var newInstructor = new Instructor
                    {
                        Name = nameBox.Text,
                        Department = "N/A"
                    };

                    context.Instructors.Add(newInstructor);
                    context.SaveChanges();

                    MessageBox.Show("Instructor added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (DbUpdateException dbEx)
                {
                    MessageBox.Show($"A database error occurred: {dbEx.InnerException?.Message ?? dbEx.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Submission Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            fetch_instructors();
            ClearInputs();
        }

        // ===================================================================
        // Navigation Button Click Handlers
        // ===================================================================

        private void button1_Click(object sender, EventArgs e)
        {
            active_space = "courses";
            fetch_courses();
            ClearInputs();
            (sender as Button)?.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Student Management has been disabled.", "Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
            active_space = "courses";
            fetch_courses();
            (sender as Button)?.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            active_space = "instructors";
            fetch_instructors();
            ClearInputs();
            (sender as Button)?.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            switch (active_space)
            {
                case "courses":
                    AddCourse();
                    break;
                case "instructors":
                    AddInstructor();
                    break;
                default:
                    MessageBox.Show($"Unknown active space: {active_space}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a record to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var context = new ApplicationDbContext())
            {
                try
                {
                    if (active_space == "courses")
                    {
                        int courseId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["CourseId"].Value);

                        var course = context.Courses.Find(courseId);
                        if (course != null)
                        {
                            context.Courses.Remove(course);
                            context.SaveChanges();

                            MessageBox.Show("Course deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        fetch_courses();
                    }
                    else if (active_space == "instructors")
                    {
                        int instructorId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["InstructorId"].Value);

                        var instructor = context.Instructors
                            .Include(i => i.TaughtCourses)
                            .FirstOrDefault(i => i.InstructorId == instructorId);

                        if (instructor != null)
                        {
                            foreach (var course in instructor.TaughtCourses.ToList())
                            {
                                course.InstructorId = null; // requires Course.InstructorId to be nullable
                            }

                            context.Instructors.Remove(instructor);
                            context.SaveChanges();

                            MessageBox.Show("Instructor deleted successfully! Their courses were unassigned.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        fetch_instructors();
                    }
                    else
                    {
                        MessageBox.Show($"Delete not supported for {active_space}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting record: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
