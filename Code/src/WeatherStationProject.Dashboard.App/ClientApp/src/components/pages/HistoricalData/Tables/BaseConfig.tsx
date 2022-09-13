import moment from 'moment/moment';
import { TFunction } from 'react-i18next';
import React from 'react';

export const dateFormatter = (cell: never) => {
  return moment(cell).format('DD-MM-YYYY hh:mm:ss');
};

export const getDateTimeColumn = (t: TFunction, columnName = 'dateTime') => {
  return {
    dataField: columnName,
    text: t('historical_data.list.datetime'),
    sort: true,
    headerAlign: 'center',
    formatter: dateFormatter,
    csvFormatter: dateFormatter,
  };
};

export const getColumn = (field: string, text: string, color: string) => {
  return {
    dataField: field,
    text: text,
    sort: true,
    headerAlign: 'center',
    headerStyle: {
      backgroundColor: color,
    },
  };
};

const customTotal = (from: number, to: number, size: number) => (
  <span className="react-bootstrap-table-pagination-total ml-3">
    Showing {from} to {to} of {size} results
  </span>
);

export const paginationOptions = {
  paginationSize: 4,
  pageStartIndex: 0,
  showTotal: true,
  paginationTotalRenderer: customTotal,
  disablePageTitle: true,
  sizePerPageList: [5, 10, 25],
};
