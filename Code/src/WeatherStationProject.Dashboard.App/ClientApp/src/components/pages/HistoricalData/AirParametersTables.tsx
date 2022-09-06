import { IAirParameters } from '../../../model/LastDataTypes';
import React from 'react';
import { useTranslation } from 'react-i18next';
import BootstrapTable from 'react-bootstrap-table-next';
import paginationFactory, { PaginationProvider, PaginationListStandalone } from 'react-bootstrap-table2-paginator';
import filterFactory, { textFilter, selectFilter, dateFilter, Comparator } from 'react-bootstrap-table2-filter';

interface IAirParametersTablesProps {
  measurements: IAirParameters[];
}

const AirParametersTables: React.FC<IAirParametersTablesProps> = ({ measurements }) => {
  const { t } = useTranslation();
  const columns = [
    {
      dataField: 'dateTime',
      text: 'Datetime',
      filter: dateFilter({
        defaultValue: { date: new Date(), comparator: Comparator.LE },
      }),
    },
    {
      dataField: 'pressure',
      text: 'Pressure',
      filter: textFilter(),
    },
    {
      dataField: 'humidity',
      text: 'Humidity',
      filter: textFilter(),
    },
  ];
  const customTotal = (from: number, to: number, size: number) => (
    <span className="react-bootstrap-table-pagination-total">
      Showing {from} to {to} of {size} Results
    </span>
  );
  const options = {
    paginationSize: 4,
    pageStartIndex: 0,
    // alwaysShowAllBtns: true, // Always show next and previous button
    // withFirstAndLast: false, // Hide the going to First and Last page button
    // hideSizePerPage: true, // Hide the sizePerPage dropdown always
    // hidePageListOnlyOnePage: true, // Hide the pagination list when only one page
    firstPageText: 'First',
    prePageText: 'Back',
    nextPageText: 'Next',
    lastPageText: 'Last',
    nextPageTitle: 'First page',
    prePageTitle: 'Pre page',
    firstPageTitle: 'Next page',
    lastPageTitle: 'Last page',
    showTotal: true,
    paginationTotalRenderer: customTotal,
    disablePageTitle: true,
    sizePerPageList: [
      {
        text: '5',
        value: 5,
      },
      {
        text: '10',
        value: 10,
      },
      {
        text: 'All',
        value: measurements.length,
      },
    ], // A numeric array is also available. the purpose of above example is custom the text
  };

  return (
    <BootstrapTable
      keyField="id"
      data={measurements}
      columns={columns}
      filter={filterFactory()}
      pagination={paginationFactory(options)}
    />
  );
};

export default AirParametersTables;
