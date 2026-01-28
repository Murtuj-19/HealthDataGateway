# Health Data Gateway - Module UI Guide

## Overview
Each module (Connector, Source Hospital, Target Hospital) now has a professional side navigation UI with responsive design.

## New Layout Architecture

### Sidebar Navigation (`_LayoutWithSidebar.cshtml`)
- **Fixed top header** with branding and user info
- **Left sidebar** with role-based navigation (260px wide, collapsible on mobile)
- **Main content area** with full-width pages
- **Footer** at the bottom
- **Mobile responsive** - sidebar becomes hamburger menu on screens < 768px

## Module Navigation Links

### Connector Gateway
Navigation items:
- Dashboard (active)
- Pending Requests
- Delivered
- Failed
- Reports

### Source Hospital
Navigation items:
- Dashboard (active)
- Patients
- Add Patient
- Transfer Requests
- Reports

### Target Hospital
Navigation items:
- Dashboard (active)
- Incoming Requests
- Pending Review
- Accepted
- Reports

---

## Implementation Details

### How to Apply Sidebar Layout
Add this line to any page that needs the sidebar:
```razor
@{
    Layout = "~/Pages/Shared/_LayoutWithSidebar.cshtml";
}
```

### Page Header Structure
Use this on every page for consistency:
```razor
<div class="page-header">
    <h1 class="page-title">
        <i class="bi bi-icon-name"></i> Page Title
    </h1>
    <p class="page-subtitle">Descriptive subtitle</p>
</div>
```

### Adding Navigation Links (In Sidebar)
Edit `_LayoutWithSidebar.cshtml` to add more links:
```html
<a href="/Module/Page" class="sidebar-nav-link">
    <i class="bi bi-icon"></i>
    <span>Link Text</span>
</a>
```

---

## Styling Components Available

### Statistics Cards
```html
<div class="stat-card">
    <i class="bi bi-icon" style="font-size: 2.5rem; color: #667eea;"></i>
    <div class="stat-label">Label</div>
    <div class="stat-number" style="color: #667eea;">0</div>
</div>
```

### Status Badges
```html
<span class="status-badge-custom status-pending">PENDING</span>
<span class="status-badge-custom status-accepted">ACCEPTED</span>
<span class="status-badge-custom status-rejected">REJECTED</span>
```

### Alerts
```html
<div class="alert alert-success alert-dismissible fade show">
    <i class="bi bi-check-circle-fill"></i>
    <strong>Success!</strong> Message here
    <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
</div>
```

---

## Color Scheme

- **Primary Gradient:** `#667eea ? #764ba2` (Purple/Blue)
- **Success:** `#27ae60` (Green)
- **Warning:** `#f5576c` (Red)
- **Info:** `#4facfe` (Cyan)
- **Connector:** `#2c3e50` (Dark)

---

## Responsive Behavior

**Desktop (>768px):**
- Sidebar always visible on left
- Full navigation with hover effects
- 260px sidebar width

**Tablet/Mobile (<768px):**
- Hamburger menu button appears in header
- Sidebar slides in from left (overlay)
- Closes automatically when link clicked
- Full-width content area

---

## Adding New Pages to Modules

1. Create new page in module folder
2. Add layout reference at top:
   ```razor
   @{
       Layout = "~/Pages/Shared/_LayoutWithSidebar.cshtml";
   }
   ```
3. Add new navigation link in sidebar
4. Create page header with title and subtitle
5. Add content below

---

## Key Features

? **Role-Based Navigation** - Different menus for each module  
? **Active Link Highlighting** - Current page highlighted in sidebar  
? **User Info Display** - Shows logged-in user and role  
? **Mobile Responsive** - Hamburger menu on smaller screens  
? **Professional Styling** - Gradient backgrounds and smooth transitions  
? **Quick Access Buttons** - User profile and logout in header  
? **Consistent Layout** - Same structure across all modules  

---

## Testing the UI

1. **Login as Connector:**
   - Navigate to Connector dashboard
   - See Connector-specific sidebar menu

2. **Login as Source Hospital:**
   - Navigate to Source Hospital dashboard
   - See Source Hospital-specific sidebar menu

3. **Login as Target Hospital:**
   - Navigate to Target Hospital dashboard
   - See Target Hospital-specific sidebar menu

4. **Mobile Test:**
   - Resize browser < 768px
   - Click hamburger menu icon
   - Sidebar slides in from left
   - Navigation links work properly

---

## Future Enhancements

- Add "Analytics" section with charts
- Implement "Settings" page
- Add "Notifications" bell icon
- Add "Search" functionality in header
- Add "Recent Activity" in sidebar
- Implement "Dark Mode" toggle
