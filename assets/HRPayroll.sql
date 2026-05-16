CREATE TABLE [companies] (
  [id] uuid PRIMARY KEY,
  [name] nvarchar(255) NOT NULL,
  [logo_url] nvarchar(255),
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [users] (
  [id] uuid PRIMARY KEY,
  [company_id] uuid NOT NULL,
  [email] nvarchar(255) UNIQUE NOT NULL,
  [first_name] nvarchar(255) NOT NULL,
  [last_name] nvarchar(255) NOT NULL,
  [role] nvarchar(255) NOT NULL,
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [branches] (
  [id] uuid PRIMARY KEY,
  [company_id] uuid NOT NULL,
  [name] nvarchar(255) NOT NULL,
  [address] nvarchar(255),
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [grades] (
  [id] uuid PRIMARY KEY,
  [name] nvarchar(255) NOT NULL,
  [min_salary] decimal NOT NULL,
  [max_salary] decimal NOT NULL,
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [job_titles] (
  [id] uuid PRIMARY KEY,
  [grade_id] uuid NOT NULL,
  [name] nvarchar(255) NOT NULL,
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [departments] (
  [id] uuid PRIMARY KEY,
  [company_id] uuid NOT NULL,
  [manager_id] uuid,
  [name] nvarchar(255) NOT NULL,
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [employees] (
  [id] uuid PRIMARY KEY,
  [user_id] uuid NOT NULL,
  [department_id] uuid,
  [branch_id] uuid,
  [job_title_id] uuid,
  [salary_structure_id] uuid,
  [employee_code] nvarchar(255) UNIQUE NOT NULL,
  [hire_date] date NOT NULL,
  [status] nvarchar(255) NOT NULL,
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [employee_personal_info] (
  [id] uuid PRIMARY KEY,
  [employee_id] uuid NOT NULL,
  [date_of_birth] date,
  [gender] nvarchar(255),
  [national_id] nvarchar(255),
  [phone] nvarchar(255),
  [address] nvarchar(255),
  [nationality] nvarchar(255),
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [employee_emergency_contacts] (
  [id] uuid PRIMARY KEY,
  [employee_id] uuid NOT NULL,
  [full_name] nvarchar(255) NOT NULL,
  [relationship] nvarchar(255) NOT NULL,
  [phone] nvarchar(255) NOT NULL,
  [email] nvarchar(255),
  [is_primary] boolean NOT NULL DEFAULT (false),
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [employment_histories] (
  [id] uuid PRIMARY KEY,
  [employee_id] uuid NOT NULL,
  [change_type] nvarchar(255) NOT NULL,
  [old_value] nvarchar(255),
  [new_value] nvarchar(255),
  [effective_date] date NOT NULL,
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [documents] (
  [id] uuid PRIMARY KEY,
  [employee_id] uuid NOT NULL,
  [type] nvarchar(255) NOT NULL,
  [file_name] nvarchar(255) NOT NULL,
  [file_url] nvarchar(255) NOT NULL,
  [uploaded_by] uuid NOT NULL,
  [expiry_date] date,
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [leave_types] (
  [id] uuid PRIMARY KEY,
  [name] nvarchar(255) NOT NULL,
  [default_days_per_year] int NOT NULL,
  [is_paid] boolean NOT NULL DEFAULT (true),
  [allow_carry_forward] boolean NOT NULL DEFAULT (false),
  [max_carry_forward_days] int,
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [leave_policies] (
  [id] uuid PRIMARY KEY,
  [company_id] uuid NOT NULL,
  [leave_type_id] uuid NOT NULL,
  [grade_id] uuid,
  [allowed_days] int NOT NULL,
  [effective_from] date NOT NULL,
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [leave_balances] (
  [id] uuid PRIMARY KEY,
  [employee_id] uuid NOT NULL,
  [leave_type_id] uuid NOT NULL,
  [year] int NOT NULL,
  [total_days] int NOT NULL,
  [carried_forward_days] int NOT NULL DEFAULT (0),
  [used_days] int NOT NULL DEFAULT (0),
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [leave_requests] (
  [id] uuid PRIMARY KEY,
  [employee_id] uuid NOT NULL,
  [leave_type_id] uuid NOT NULL,
  [start_date] date NOT NULL,
  [end_date] date NOT NULL,
  [total_days] int NOT NULL,
  [reason] nvarchar(255),
  [status] nvarchar(255) NOT NULL,
  [reviewed_by] uuid,
  [reviewed_at] timestamp,
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [attendance_records] (
  [id] uuid PRIMARY KEY,
  [employee_id] uuid NOT NULL,
  [date] date NOT NULL,
  [check_in] time,
  [check_out] time,
  [total_hours] decimal,
  [status] nvarchar(255) NOT NULL,
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [public_holidays] (
  [id] uuid PRIMARY KEY,
  [company_id] uuid NOT NULL,
  [name] nvarchar(255) NOT NULL,
  [date] date NOT NULL,
  [is_recurring] boolean NOT NULL DEFAULT (false),
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [salary_components] (
  [id] uuid PRIMARY KEY,
  [name] nvarchar(255) NOT NULL,
  [type] nvarchar(255) NOT NULL,
  [calculation_type] nvarchar(255) NOT NULL,
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [salary_structures] (
  [id] uuid PRIMARY KEY,
  [name] nvarchar(255) NOT NULL,
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [salary_structure_components] (
  [salary_structure_id] uuid NOT NULL,
  [salary_component_id] uuid NOT NULL,
  [value] decimal NOT NULL,
  [display_order] int NOT NULL DEFAULT (0),
  PRIMARY KEY ([salary_structure_id], [salary_component_id])
)
GO

CREATE TABLE [payroll_runs] (
  [id] uuid PRIMARY KEY,
  [company_id] uuid NOT NULL,
  [month] int NOT NULL,
  [year] int NOT NULL,
  [status] nvarchar(255) NOT NULL,
  [approved_by] uuid,
  [approved_at] timestamp,
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [payslips] (
  [id] uuid PRIMARY KEY,
  [payroll_run_id] uuid NOT NULL,
  [employee_id] uuid NOT NULL,
  [basic_salary] decimal NOT NULL,
  [total_allowances] decimal NOT NULL,
  [total_deductions] decimal NOT NULL,
  [net_salary] decimal NOT NULL,
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [payslip_lines] (
  [id] uuid PRIMARY KEY,
  [payslip_id] uuid NOT NULL,
  [component_name] nvarchar(255) NOT NULL,
  [type] nvarchar(255) NOT NULL,
  [amount] decimal NOT NULL,
  [display_order] int NOT NULL DEFAULT (0),
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE TABLE [one_time_adjustments] (
  [id] uuid PRIMARY KEY,
  [employee_id] uuid NOT NULL,
  [payroll_run_id] uuid,
  [month] int NOT NULL,
  [year] int NOT NULL,
  [type] nvarchar(255) NOT NULL,
  [amount] decimal NOT NULL,
  [reason] nvarchar(255),
  [created_at] timestamp NOT NULL DEFAULT (now()),
  [created_by] uuid,
  [modified_at] timestamp,
  [modified_by] uuid,
  [is_deleted] boolean NOT NULL DEFAULT (false)
)
GO

CREATE UNIQUE INDEX [leave_balances_index_0] ON [leave_balances] ("employee_id", "leave_type_id", "year")
GO

CREATE UNIQUE INDEX [attendance_records_index_1] ON [attendance_records] ("employee_id", "date")
GO

CREATE UNIQUE INDEX [public_holidays_index_2] ON [public_holidays] ("company_id", "date")
GO

CREATE UNIQUE INDEX [payroll_runs_index_3] ON [payroll_runs] ("company_id", "month", "year")
GO

CREATE UNIQUE INDEX [payslips_index_4] ON [payslips] ("payroll_run_id", "employee_id")
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'SuperAdmin | HRManager | HROfficer | Employee',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'users',
@level2type = N'Column', @level2name = 'role';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Active | OnLeave | Terminated',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'employees',
@level2type = N'Column', @level2name = 'status';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Male | Female | Other',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'employee_personal_info',
@level2type = N'Column', @level2name = 'gender';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Spouse | Parent | Sibling | Friend | Other',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'employee_emergency_contacts',
@level2type = N'Column', @level2name = 'relationship';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Hired | Promoted | Transferred | Terminated',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'employment_histories',
@level2type = N'Column', @level2name = 'change_type';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Contract | ID | Certificate | Other',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'documents',
@level2type = N'Column', @level2name = 'type';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'null = unlimited carry forward',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'leave_types',
@level2type = N'Column', @level2name = 'max_carry_forward_days';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'null = applies to all grades',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'leave_policies',
@level2type = N'Column', @level2name = 'grade_id';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Pending | Approved | Rejected',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'leave_requests',
@level2type = N'Column', @level2name = 'status';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Computed from check_in and check_out',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'attendance_records',
@level2type = N'Column', @level2name = 'total_hours';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Present | Absent | Late | HalfDay',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'attendance_records',
@level2type = N'Column', @level2name = 'status';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'If true, repeats every year on same date',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'public_holidays',
@level2type = N'Column', @level2name = 'is_recurring';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Allowance | Deduction',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'salary_components',
@level2type = N'Column', @level2name = 'type';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Fixed | PercentageOfBasic',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'salary_components',
@level2type = N'Column', @level2name = 'calculation_type';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Draft | Approved | Finalized',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'payroll_runs',
@level2type = N'Column', @level2name = 'status';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Allowance | Deduction',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'payslip_lines',
@level2type = N'Column', @level2name = 'type';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'null until included in a payroll run',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'one_time_adjustments',
@level2type = N'Column', @level2name = 'payroll_run_id';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Bonus | Deduction',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'one_time_adjustments',
@level2type = N'Column', @level2name = 'type';
GO

ALTER TABLE [users] ADD FOREIGN KEY ([company_id]) REFERENCES [companies] ([id])
GO

ALTER TABLE [branches] ADD FOREIGN KEY ([company_id]) REFERENCES [companies] ([id])
GO

ALTER TABLE [job_titles] ADD FOREIGN KEY ([grade_id]) REFERENCES [grades] ([id])
GO

ALTER TABLE [departments] ADD FOREIGN KEY ([company_id]) REFERENCES [companies] ([id])
GO

ALTER TABLE [departments] ADD FOREIGN KEY ([manager_id]) REFERENCES [employees] ([id])
GO

ALTER TABLE [employees] ADD FOREIGN KEY ([user_id]) REFERENCES [users] ([id])
GO

ALTER TABLE [employees] ADD FOREIGN KEY ([department_id]) REFERENCES [departments] ([id])
GO

ALTER TABLE [employees] ADD FOREIGN KEY ([branch_id]) REFERENCES [branches] ([id])
GO

ALTER TABLE [employees] ADD FOREIGN KEY ([job_title_id]) REFERENCES [job_titles] ([id])
GO

ALTER TABLE [employees] ADD FOREIGN KEY ([salary_structure_id]) REFERENCES [salary_structures] ([id])
GO

ALTER TABLE [employee_personal_info] ADD FOREIGN KEY ([employee_id]) REFERENCES [employees] ([id])
GO

ALTER TABLE [employee_emergency_contacts] ADD FOREIGN KEY ([employee_id]) REFERENCES [employees] ([id])
GO

ALTER TABLE [employment_histories] ADD FOREIGN KEY ([employee_id]) REFERENCES [employees] ([id])
GO

ALTER TABLE [documents] ADD FOREIGN KEY ([employee_id]) REFERENCES [employees] ([id])
GO

ALTER TABLE [documents] ADD FOREIGN KEY ([uploaded_by]) REFERENCES [users] ([id])
GO

ALTER TABLE [leave_policies] ADD FOREIGN KEY ([company_id]) REFERENCES [companies] ([id])
GO

ALTER TABLE [leave_policies] ADD FOREIGN KEY ([leave_type_id]) REFERENCES [leave_types] ([id])
GO

ALTER TABLE [leave_policies] ADD FOREIGN KEY ([grade_id]) REFERENCES [grades] ([id])
GO

ALTER TABLE [leave_balances] ADD FOREIGN KEY ([employee_id]) REFERENCES [employees] ([id])
GO

ALTER TABLE [leave_balances] ADD FOREIGN KEY ([leave_type_id]) REFERENCES [leave_types] ([id])
GO

ALTER TABLE [leave_requests] ADD FOREIGN KEY ([employee_id]) REFERENCES [employees] ([id])
GO

ALTER TABLE [leave_requests] ADD FOREIGN KEY ([leave_type_id]) REFERENCES [leave_types] ([id])
GO

ALTER TABLE [leave_requests] ADD FOREIGN KEY ([reviewed_by]) REFERENCES [users] ([id])
GO

ALTER TABLE [attendance_records] ADD FOREIGN KEY ([employee_id]) REFERENCES [employees] ([id])
GO

ALTER TABLE [public_holidays] ADD FOREIGN KEY ([company_id]) REFERENCES [companies] ([id])
GO

ALTER TABLE [salary_structure_components] ADD FOREIGN KEY ([salary_structure_id]) REFERENCES [salary_structures] ([id])
GO

ALTER TABLE [salary_structure_components] ADD FOREIGN KEY ([salary_component_id]) REFERENCES [salary_components] ([id])
GO

ALTER TABLE [payroll_runs] ADD FOREIGN KEY ([company_id]) REFERENCES [companies] ([id])
GO

ALTER TABLE [payroll_runs] ADD FOREIGN KEY ([approved_by]) REFERENCES [users] ([id])
GO

ALTER TABLE [payslips] ADD FOREIGN KEY ([payroll_run_id]) REFERENCES [payroll_runs] ([id])
GO

ALTER TABLE [payslips] ADD FOREIGN KEY ([employee_id]) REFERENCES [employees] ([id])
GO

ALTER TABLE [payslip_lines] ADD FOREIGN KEY ([payslip_id]) REFERENCES [payslips] ([id])
GO

ALTER TABLE [one_time_adjustments] ADD FOREIGN KEY ([employee_id]) REFERENCES [employees] ([id])
GO

ALTER TABLE [one_time_adjustments] ADD FOREIGN KEY ([payroll_run_id]) REFERENCES [payroll_runs] ([id])
GO
