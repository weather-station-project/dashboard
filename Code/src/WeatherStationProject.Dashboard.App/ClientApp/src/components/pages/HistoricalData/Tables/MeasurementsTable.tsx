import React from 'react';
import { useTranslation } from 'react-i18next';
import BootstrapTable, { ColumnDescription } from 'react-bootstrap-table-next';
import paginationFactory from 'react-bootstrap-table2-paginator';
import ToolkitProvider, { CSVExport, ToolkitContextType } from 'react-bootstrap-table2-toolkit';
import { paginationOptions } from './BaseConfig';

interface IMeasurementsTablesProps {
  measurements: object[];
  columns: object[];
  columnNameSort: string;
}

// Doc -> https://react-bootstrap-table.github.io/react-bootstrap-table2/

const MeasurementsTable: React.FC<IMeasurementsTablesProps> = ({ measurements, columns, columnNameSort }) => {
  const { t } = useTranslation();
  const { ExportCSVButton } = CSVExport;

  return (
    <ToolkitProvider bootstrap4 keyField="id" data={measurements} columns={columns as ColumnDescription[]} exportCSV>
      {(props: ToolkitContextType) => (
        <div>
          <ExportCSVButton {...props.csvProps}>{t('historical_data.list.export_csv')}</ExportCSVButton>
          <hr />
          <BootstrapTable
            {...props.baseProps}
            pagination={paginationFactory(paginationOptions)}
            defaultSorted={[
              {
                dataField: columnNameSort,
                order: 'desc',
              },
            ]}
          />
        </div>
      )}
    </ToolkitProvider>
  );
};

export default MeasurementsTable;
