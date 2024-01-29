import { useMemo, useState, useEffect } from "react";
import {
  MaterialReactTable,
  useMaterialReactTable,
} from "material-react-table";
import { Button, IconButton, Tooltip } from "@mui/material";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { MRT_Localization_FA } from "material-react-table/locales/fa";
import {
  Edit as EditIcon,
  Delete as DeleteIcon,
  Refresh as RefreshIcon,
  Email as EmailIcon,
  MobileOff as MobileOffIcon,
  MobileFriendly as MobileFriendlyIcon,
  VerifiedUser as VerifiedUserIcon,
} from "@mui/icons-material/";
import Swal from "sweetalert2";

const TableGrid = ({
  isRefetching,
  onSetIsRefetching,
  onOpenModal
}) => {
  const [data, setData] = useState([]);
  const [isError, setIsError] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [rowCount, setRowCount] = useState(0);

  const [columnFilters, setColumnFilters] = useState([]);
  const [globalFilter, setGlobalFilter] = useState("");
  const [sorting, setSorting] = useState([]);
  const [validationErrors, setValidationErrors] = useState({});
  const [pagination, setPagination] = useState({
    pageIndex: 0,
    pageSize: 10,
  });

  useEffect(() => {
    const fetchData = async () => {
      if (!data.length) {
        setIsLoading(true);
      } else {
        onSetIsRefetching(true);
      }

      const fetchURL = new URL(
        "/api/ticket/get",
        process.env.NODE_ENV === "production"
          ? "https://www.material-react-table.com"
          : "https://localhost:5001"
      );

      fetchURL.searchParams.set(
        "start",
        `${pagination.pageIndex * pagination.pageSize}`
      );
      fetchURL.searchParams.set("size", `${pagination.pageSize}`);
      fetchURL.searchParams.set("filters", JSON.stringify(columnFilters ?? []));
      fetchURL.searchParams.set("globalFilter", globalFilter ?? "");
      fetchURL.searchParams.set("sorting", JSON.stringify(sorting ?? []));

      var myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");
      var raw = JSON.stringify({
        start: Number(fetchURL.searchParams.get("start")),
        size: Number(fetchURL.searchParams.get("size")),
        globalFilter: fetchURL.searchParams.get("globalFilter"),
        filters: JSON.parse(fetchURL.searchParams.get("filters")),
        sorting: JSON.parse(fetchURL.searchParams.get("sorting"))[0],
      });
      var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
      };

      try {
        const response = await fetch("/api/ticket/get", requestOptions);
        const json = await response.json();
        const result = json.Data.Data.Data.map((item) => {
          debugger;
          return {
            id: item.TicketId,
            ticketNumber: item.TicketNumber,
            ticketDate: item.TicketDate,
            customerName: item.Customer.LastName,
            customerPhone: item.Customer.User.PhoneNumber,
            customerEmail: item.Person.User.Email,
          };
        });

        setData(result);
        setRowCount(json.Data.Data.Total);
      } catch (error) {
        setIsError(true);
        console.error(error);
        return;
      }
      setIsError(false);
      setIsLoading(false);
      onSetIsRefetching(false);
    };
    fetchData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [
    isRefetching,
    columnFilters,
    globalFilter,
    pagination.pageIndex,
    pagination.pageSize,
    sorting,
  ]);

  const columns = useMemo(
    () => [
      {
        accessorKey: "ticketNumber",
        header: "شماره تیکت",
        muiEditTextFieldProps: {
          type: "number",
          required: true,
          error: !!validationErrors?.firstName,
          helperText: validationErrors?.firstName,
          //remove any previous validation errors when user focuses on the input
          onFocus: () =>
            setValidationErrors({
              ...validationErrors,
              firstName: undefined,
            }),
          //optionally add validation checking for onBlur or onChange
        },
      },
      {
        accessorKey: "ticketDate",
        header: "تاریخ تیکت",
        muiEditTextFieldProps: {
          type: "text",
          required: true,
          error: !!validationErrors?.lastName,
          helperText: validationErrors?.lastName,
          //remove any previous validation errors when user focuses on the input
          onFocus: () =>
            setValidationErrors({
              ...validationErrors,
              lastName: undefined,
            }),
        },
      },
      
      {
        accessorKey: "customer",
        header: "مشتری",
        muiEditTextFieldProps: {
          type: "text",
          required: true,
          error: !!validationErrors?.email,
          helperText: validationErrors?.email,
          //remove any previous validation errors when user focuses on the input
          onFocus: () =>
            setValidationErrors({
              ...validationErrors,
              email: undefined,
            }),
        },
      },

      {
        accessorKey: "phoneNumber",
        header: "شماره تماس",
        muiEditTextFieldProps: {
          type: "phone",
          required: true,
          error: !!validationErrors?.phoneNumber,
          helperText: validationErrors?.phoneNumber,
          //remove any previous validation errors when user focuses on the input
          onFocus: () =>
            setValidationErrors({
              ...validationErrors,
              phoneNumber: undefined,
            }),
        },
      },
    ],
    [validationErrors]
  );

  const handleDeleteUser = (ticket) => {
    Swal.fire({
      title: `آیا از حذف تیکت ${ticket.number} اطمینان دارید؟`,
      text: "در صورت حذف امکان بازگشت وجود ندارد",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "بله",
      cancelButtonText: "خیر",
    }).then((result) => {
      if (result.isConfirmed) {
        var requestOptions = {
          method: "DELETE",
          redirect: "follow",
        };

        fetch(`/api/ticket/delete?id=${ticket.id}`, requestOptions)
          .then(() => {
            Swal.fire({
              title: `تیکت ${ticket.number} حذف شد`,
              icon: "success",
            });
            onSetIsRefetching(true);
          })
          .catch((error) => console.log("error", error));
      }
    });
  };

  const table = useMaterialReactTable({
    columns,
    data,
    manualFiltering: true,
    manualPagination: true,
    manualSorting: true,
    enableFullScreenToggle: false,
    localization: MRT_Localization_FA,
    createDisplayMode: "",
    enableEditing: true,
    positionActionsColumn: "last",
    onColumnFiltersChange: setColumnFilters,
    onGlobalFilterChange: setGlobalFilter,
    onPaginationChange: setPagination,
    onSortingChange: setSorting,
    rowCount,
    state: {
      columnFilters,
      globalFilter,
      isLoading,
      pagination,
      showAlertBanner: isError,
      showProgressBars: isRefetching,
      sorting,
    },
    displayColumnDefOptions: {
      "mrt-row-actions": {
        size: 150,
        Cell: ({ row }) => (
          <>
            <Button onClick={() => onOpenModal("addedit", row.original)}>
              <EditIcon />
            </Button>
            <Button
              color={row.original.lockoutEnabled ? "error" : "success"}
              onClick={() => onOpenModal("useraccess", row.original)}
            >
              <VerifiedUserIcon />
            </Button>
            <Button
              color={row.original.emailConfirmed ? "success" : "error"}
              onClick={() => {
                if (row.original.emailConfirmed) {
                  Swal.fire({
                    icon: "info",
                    text: "ایمیل قبلا تایید شده است. در صورت تغییر آن به تایید مجدد نیاز میباشد.",
                  });
                  return;
                }
                onOpenModal("emailconfirm", row.original);
              }}
            >
              <EmailIcon />
            </Button>
            <Button
              color={row.original.phoneConfirmed ? "success" : "error"}
              onClick={() => {
                if (row.original.phoneConfirmed) {
                  Swal.fire({
                    icon: "info",
                    text: "شماره تماس قبلا تایید شده است. در صورت تغییر آن به تایید مجدد نیاز میباشد.",
                  });
                  return;
                }
                onOpenModal("phoneconfirm", row.original);
              }}
            >
              {row.original.phoneConfirmed ? (
                <MobileFriendlyIcon />
              ) : (
                <MobileOffIcon />
              )}
            </Button>
            <Button
              color="error"
              onClick={() => handleDeleteUser(row.original)}
            >
              <DeleteIcon />
            </Button>
          </>
        ),
      },
    },
    renderTopToolbarCustomActions: () => (
      <>
        <Button variant="contained" onClick={() => onOpenModal("addedit")}>
          تیکت جدید
        </Button>
        <Tooltip arrow title="Refresh Data">
          <IconButton onClick={() => onSetIsRefetching(true)}>
            <RefreshIcon />
          </IconButton>
        </Tooltip>
      </>
    ),
  });

  return <MaterialReactTable table={table} />;
};

const queryClient = new QueryClient();

const TicketDataGrid = (props) => (
  <QueryClientProvider client={queryClient}>
    <TableGrid
      onOpenModal={(type, payload) => props.modalHandler(type, payload)}
      {...props}
    />
  </QueryClientProvider>
);

export default TicketDataGrid;
